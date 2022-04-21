using Server;
using Grpc.Net.Client;

namespace MobileTest
{
    public partial class frmMain : Form
    {
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
                lblLogin.Text = "Resultado: " + reply.LoginEfetuado.ToString() + " Tipo de utilizador = " + reply.TipoUser.ToString();
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
            var reply = await client.ChangePasswordAsync(input);
            lblMudaPass.Text = "Resultado: " + reply.PassAlterada.ToString();
        }
    }
}