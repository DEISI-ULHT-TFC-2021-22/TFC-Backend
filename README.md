# TFC-Backend Parque de Estacionamento Lusofona

## Instalação
1. Instalar o Microsoft SQL server 2019 Express com o nome do servidor 'PARKINGLOTDB' e com a conta de 'sa' com a password 'Lusofona'

2. Instalar o Microsoft SQL Server Management Studio (Microsoft SSMS)

3. No Microsoft SSMS fazer login com a conta de 'sa'                                                          
![db4](https://user-images.githubusercontent.com/104139081/180337974-4335d5f2-d09f-4a2b-b4ca-bbb1807955dc.png)

4. Pressinar com a tecla do lado direito do rato em cima da pasta 'Databases' e selecionar 'New Database...'
![db1](https://user-images.githubusercontent.com/104139081/180336932-033ef829-88af-4e66-b292-eb5dc09f3d35.png)

5. Na nova janela, no campo 'Database name' escrever 'ParkingLotDB' e depois pressionar o botão ok para criar a base de dados
![db2](https://user-images.githubusercontent.com/104139081/180336939-320f44c6-3771-4eeb-a1e4-cb1d36d63f3f.png)

6. Pressionar em 'New Query' ou Ctrl+N para criar uma nova query e arrastar o ficheiro 'script.sql' para dentro dessa janela que se encontra na raiz deste repositorio.
![db3](https://user-images.githubusercontent.com/104139081/180337564-730e390e-e798-4b03-af88-a3900fce82f2.png)

7. Depois pressionar em 'Execute' ou Alt+X para executar o script ficando assim concluida a instalação da Base de Dados

8. Instalar o visual studio 2022 com os seguintes "Pacotes"
![1](https://user-images.githubusercontent.com/104139081/164999303-e52369a6-caba-4f8d-bb90-3163e714821d.png)

9. Faça o donwload dos ficheiros deste repositório e abra o ficheiro "TFC.sln"
![2](https://user-images.githubusercontent.com/104139081/164999306-1bede529-492f-472b-bdf7-264952c3fe1e.png)

10. Dentro da pasta 'Server' existe a pasta 'Fotos' que deve ser partilhada. Para isso, pressionar a referida pasta 'Fotos' com a tecla do lado direito do rato e selecionar a opção 'propriedades'
![PR1](https://user-images.githubusercontent.com/104139081/180352683-8150570a-beb5-440a-a6e9-e05128a9525d.png)

11. Depois selecionar a tab  'Partilhar' pressionar o botão 'Partilha Avançada...'
![PR2](https://user-images.githubusercontent.com/104139081/180352836-5cb80718-bb6e-4f1b-a458-bab47c2d4849.png)

12. Na nova janela selecionar a check box 'Partilhar esta pasta', defenir o nome da partilha como 'Fotos' e de seguida selecionar o botão 'permissões'
![PR3](https://user-images.githubusercontent.com/104139081/180353079-55b328f7-e893-494d-8caf-db7a0da50b16.png)

13. Na janela 'Permissões para Fotos' selecionar 'Controlo total' pressionando em 'Ok' nesta e nas outras duas janelas anteriores
![PR4](https://user-images.githubusercontent.com/104139081/180353350-4ba0f6bc-0774-4e65-8373-55a1d9c8c877.png)

14. Deverá agora ser possível aceder a esta partilha de rede atráves do link '\\localhost\Fotos' no explorador do windows 
![PR5](https://user-images.githubusercontent.com/104139081/180353944-68eee164-a98f-4b0f-b879-ea7375c3338e.png)

15. Faça o download dos ficheiros do repositório do Front end deste tfc: https://github.com/DEISI-ULHT-TFC-2021-22/ParqueLusofona_Frontend
e dos ficheiros do repositorio da Deteção de Matriculas: https://github.com/DEISI-ULHT-TFC-2021-22/TFC-Deisi243-Detecao-Matriculas

16. Dentro do visual studio selecione a opção de adicionar um projeto existente e adicione o projecto do Front end      
![3](https://user-images.githubusercontent.com/104139081/164999376-b6ccdff2-6585-4629-90a0-ddb90909acf6.png)
![4](https://user-images.githubusercontent.com/104139081/164999492-4272cd9e-a062-4c9c-a51e-584ec6d8563f.png)

17. Clique com o botão direito do rato no "Solution" -> "Set Startup Projects..", escolha "Multiple startup projects" e ponha igual à figura abaixo, tendo o server de ficar em primeiro.
![5 1](https://user-images.githubusercontent.com/104139081/180333980-09499f14-820d-46cf-9b52-f50a1c008857.png)

18. Correr o projeto
![6](https://user-images.githubusercontent.com/104139081/164999518-4d32f0b1-b9d0-4955-ba17-11c6b242fde4.png)

19. Exemplo de utilização: https://youtu.be/rVSVYDrFEsM
