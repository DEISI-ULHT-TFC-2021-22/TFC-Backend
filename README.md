# TFC-Backend Parque de Estacionamento Lusofona

## Instalação
1. Instalar o Microsoft SQL server 2019 Express com o nome do servidor 'PARKINGLOTDB' e com a conta de 'sa' com a password 'Lusofona'

2. Instalar o Microsoft SQL Server Management Studio (Microsoft SSMS)

3. No Microsoft SSMS fazer login com a conta de 'sa'

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

10. Faça o download dos ficheiros do repositório do Front end deste tfc: https://github.com/DEISI-ULHT-TFC-2021-22/ParqueLusofona_Frontend
e dos ficheiros do repositorio da Deteção de Matriculas: https://github.com/DEISI-ULHT-TFC-2021-22/TFC-Deisi243-Detecao-Matriculas

11. Dentro do visual studio selecione a opção de adicionar um projeto existente e adicione o projecto do Front end
![3](https://user-images.githubusercontent.com/104139081/164999376-b6ccdff2-6585-4629-90a0-ddb90909acf6.png)
![4](https://user-images.githubusercontent.com/104139081/164999492-4272cd9e-a062-4c9c-a51e-584ec6d8563f.png)

12. Clique com o botão direito do rato no "Solution" -> "Set Startup Projects..", escolha "Multiple startup projects" e ponha igual à figura abaixo, tendo o server de ficar em primeiro.
![5 1](https://user-images.githubusercontent.com/104139081/180333980-09499f14-820d-46cf-9b52-f50a1c008857.png)

13. Corra o projeto
![6](https://user-images.githubusercontent.com/104139081/164999518-4d32f0b1-b9d0-4955-ba17-11c6b242fde4.png)

14. Exemplo de utilização: https://youtu.be/rVSVYDrFEsM
