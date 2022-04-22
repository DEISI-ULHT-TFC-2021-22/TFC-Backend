using Server;
using Grpc.Net.Client;

namespace MobileTest
{
    public partial class frmMain : Form
    {
        private GrpcChannel channel1 = GrpcChannel.ForAddress(@"https://localhost:7025");
        private GrpcChannel? channel = null;

        public frmMain()
        {
            InitializeComponent();
            channel = GrpcChannel.ForAddress(@"https://localhost:7025");
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var client = new Login.LoginClient(channel);
            var input = new LoginData { Login = txtBxUsername.Text, Password = txtBxPassword.Text };

            try
            {
                var reply = await client.LoginRequestAsync(input);
                if (reply.LoginEfetuado)
                {
                    lblLogin.Text = "Resultado: Benvindo " + input.Login;
                }
                else
                {
                    lblLogin.Text = "Resultado: Login invalido";
                }
            }
            catch
            {
                lblLogin.Text = "Resultado: Server not found!";
            }
        }

        private void btnEnterPark_Click(object sender, EventArgs e)
        {
            AbrirCancela(true);
        }

        private void btnExitPark_Click(object sender, EventArgs e)
        {
            AbrirCancela(false);
        }

        private async void AbrirCancela(Boolean Entrar)
        {
            var client = new Mobile.MobileClient(channel);
            var input = new Pedido { CancelaEntrada = Entrar };
            var reply = await client.AbrirCancelaAsync(input);
            lblParkAccess.Text = "Resultado: " + reply.Mensagem;
        }

        private async void btnMudaPass_Click(object sender, EventArgs e)
        {
            var client = new Login.LoginClient(channel);
            var input = new NewLoginData { Login = "xpto", OldPass = txtBxPassAntiga.Text, NewPass = txtBxNovaPass.Text };
            if (txtBxNovaPass.Text == txtBxRepNovaPass.Text)
            {
                var reply = await client.ChangePasswordAsync(input);
                lblMudaPass.Text = "Resultado: " + reply.PassAlterada.ToString();
            }
            else
            {
                lblMudaPass.Text = "Resultado: Os campos da nova password não são iguais!";
            }
        }

        private void btnVerifParkDisp_Click(object sender, EventArgs e)
        {
            GrpcChannel channel1 = GrpcChannel.ForAddress(@"https://localhost:7203");
            var client = new Matriculas.MatriculasClient(channel1);
            var input = new ExisteDisponibilidade { DisponivelNovaEntrada = true };
            var reply = client.GetDisponibilidade(input);
            lblParkDisponibilidade.Text = "Resultado: " + reply.Disponivel.ToString();
        }
    }
}