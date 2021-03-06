USE [ParkingLotDB]
GO
/****** Object:  Table [dbo].[Matriculas]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matriculas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Utilizador] [int] NOT NULL,
	[Matricula] [varchar](8) NOT NULL,
 CONSTRAINT [PK_Matriculas] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permite_Entrada]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permite_Entrada](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Utilizador] [int] NOT NULL,
	[Pago_Desde] [date] NULL,
	[Pago_Ate] [date] NULL,
	[NoParque] [bit] NOT NULL,
	[ParqueGratis] [bit] NOT NULL,
 CONSTRAINT [PK_Permite_Entrada] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registos]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Matricula] [int] NOT NULL,
	[Entrada] [datetime] NULL,
	[Saida] [datetime] NULL,
 CONSTRAINT [PK_Registos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Utilizadores]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Utilizadores](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tipo_Utilizador] [varchar](15) NOT NULL,
 CONSTRAINT [PK_Tipo_Utilizadores] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utilizadores]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utilizadores](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Tipo_Utilizador] [int] NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Login] [varchar](10) NOT NULL,
	[Password] [varchar](64) NOT NULL,
	[ContaAtivada] [bit] NOT NULL,
 CONSTRAINT [PK_Utilizadores] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Matriculas] ON 

INSERT [dbo].[Matriculas] ([ID], [ID_Utilizador], [Matricula]) VALUES (1, 1, N'EN-TR-AR')
INSERT [dbo].[Matriculas] ([ID], [ID_Utilizador], [Matricula]) VALUES (2, 1, N'AA-00-00')
SET IDENTITY_INSERT [dbo].[Matriculas] OFF
GO
SET IDENTITY_INSERT [dbo].[Permite_Entrada] ON 

INSERT [dbo].[Permite_Entrada] ([ID], [ID_Utilizador], [Pago_Desde], [Pago_Ate], [NoParque], [ParqueGratis]) VALUES (1, 1, NULL, NULL, 0, 1)
SET IDENTITY_INSERT [dbo].[Permite_Entrada] OFF
GO
SET IDENTITY_INSERT [dbo].[Tipo_Utilizadores] ON 

INSERT [dbo].[Tipo_Utilizadores] ([ID], [Tipo_Utilizador]) VALUES (1, N'Admin')
INSERT [dbo].[Tipo_Utilizadores] ([ID], [Tipo_Utilizador]) VALUES (2, N'Segurança')
INSERT [dbo].[Tipo_Utilizadores] ([ID], [Tipo_Utilizador]) VALUES (3, N'Utilizador')
SET IDENTITY_INSERT [dbo].[Tipo_Utilizadores] OFF
GO
SET IDENTITY_INSERT [dbo].[Utilizadores] ON 

INSERT [dbo].[Utilizadores] ([ID], [ID_Tipo_Utilizador], [Nome], [Login], [Password], [ContaAtivada]) VALUES (1, 3, N'user', N'xpto', N'Ta77P4U1ZX9/JYukf+HMCQ==', 1)
INSERT [dbo].[Utilizadores] ([ID], [ID_Tipo_Utilizador], [Nome], [Login], [Password], [ContaAtivada]) VALUES (2, 1, N'admin', N'admin', N'nBt/za79QNqJhpM8Xy5RMw==', 1)
INSERT [dbo].[Utilizadores] ([ID], [ID_Tipo_Utilizador], [Nome], [Login], [Password], [ContaAtivada]) VALUES (5, 2, N'seg', N'seg', N'u8y9ob3hjWPSjf5iT5VSgQ==', 1)
SET IDENTITY_INSERT [dbo].[Utilizadores] OFF
GO
ALTER TABLE [dbo].[Matriculas]  WITH CHECK ADD  CONSTRAINT [FK_Matriculas_Utilizadores] FOREIGN KEY([ID_Utilizador])
REFERENCES [dbo].[Utilizadores] ([ID])
GO
ALTER TABLE [dbo].[Matriculas] CHECK CONSTRAINT [FK_Matriculas_Utilizadores]
GO
ALTER TABLE [dbo].[Permite_Entrada]  WITH CHECK ADD  CONSTRAINT [FK_Permite_Entrada_Utilizadores] FOREIGN KEY([ID_Utilizador])
REFERENCES [dbo].[Utilizadores] ([ID])
GO
ALTER TABLE [dbo].[Permite_Entrada] CHECK CONSTRAINT [FK_Permite_Entrada_Utilizadores]
GO
ALTER TABLE [dbo].[Registos]  WITH CHECK ADD  CONSTRAINT [FK_Registos_Matriculas] FOREIGN KEY([ID_Matricula])
REFERENCES [dbo].[Matriculas] ([ID])
GO
ALTER TABLE [dbo].[Registos] CHECK CONSTRAINT [FK_Registos_Matriculas]
GO
ALTER TABLE [dbo].[Utilizadores]  WITH CHECK ADD  CONSTRAINT [FK_Utilizadores_Tipo_Utilizadores] FOREIGN KEY([ID_Tipo_Utilizador])
REFERENCES [dbo].[Tipo_Utilizadores] ([ID])
GO
ALTER TABLE [dbo].[Utilizadores] CHECK CONSTRAINT [FK_Utilizadores_Tipo_Utilizadores]
GO
/****** Object:  StoredProcedure [dbo].[spMatriculas_Alterar]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spMatriculas_Alterar]
	@Login Varchar(10),
	@Matricula varchar(8)
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		declare @MatriculaExiste int

		select @MatriculaExiste = ID from Matriculas where ID_Utilizador = @UtilizadorExiste and Matricula = @Matricula

		if @MatriculaExiste > 0
		begin
			update Matriculas set Matricula = @Matricula where ID = @MatriculaExiste
			select 'true' as Resultado
		end
		else
		begin
			select 'false' as Resultado
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spMatriculas_Inserir]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spMatriculas_Inserir]
	@Login Varchar(10),
	@Matricula varchar(8)
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		declare @MatriculaExiste int

		select @MatriculaExiste = ID from Matriculas where ID_Utilizador = @UtilizadorExiste and Matricula = @Matricula

		if @MatriculaExiste > 0
		begin
			select 'false' as Resultado
		end
		else
		begin
			insert into Matriculas (ID_Utilizador, Matricula) values (@UtilizadorExiste, @Matricula)
			select 'true' as Resultado
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spMatriculas_ListarTodas]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spMatriculas_ListarTodas]
	@Login Varchar(10)
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	Begin
		select Matricula from Utilizadores, Matriculas where Utilizadores.ID = ID_Utilizador and Utilizadores.ID = @UtilizadorExiste
	End
end
GO
/****** Object:  StoredProcedure [dbo].[spMatriculas_MatriculaValida]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spMatriculas_MatriculaValida]
	@Login Varchar(10),
	@Matricula varchar(8)
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		declare @MatriculaExiste int

		select @MatriculaExiste = ID from Matriculas where ID_Utilizador = @UtilizadorExiste and Matricula = @Matricula

		if @MatriculaExiste > 0
		begin
			
			select 'true' as Resultado
		end
		else
		begin
			select 'false' as Resultado
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spMatriculas_ProcurarProprietario]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spMatriculas_ProcurarProprietario]
	@Matricula Varchar(8)
as
begin
	declare @UtilizadorExiste int;

	select @UtilizadorExiste = ID_Utilizador from Matriculas inner join Utilizadores on Utilizadores.ID = Matriculas.ID_Utilizador where Matricula = @Matricula

	if @UtilizadorExiste > 0
	begin
		select Nome, Login, ContaAtivada, Tipo_Utilizador from Utilizadores, Tipo_Utilizadores where Utilizadores.ID_Tipo_Utilizador = Tipo_Utilizadores.ID and Utilizadores.ID = @UtilizadorExiste
		select 'true' as Resultado
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spPermiteEntrada_Alterar]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spPermiteEntrada_Alterar]
	@Login Varchar(10),
	@Pago_Desde date,
	@Pago_Ate date,
	@ParqueGratis bit
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		update Permite_Entrada set Pago_Desde = @Pago_Desde, Pago_Ate = @Pago_Ate, ParqueGratis = @ParqueGratis where ID_Utilizador = @UtilizadorExiste
		select 'true' as Resultado
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spPermiteEntrada_AlterarEntradaNoParque]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spPermiteEntrada_AlterarEntradaNoParque]
	@Login Varchar(10),
	@NoParque bit
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login

	if @UtilizadorExiste > 0
	begin
		declare @Permite_EntradaExiste int;

		select @Permite_EntradaExiste = ID from Permite_Entrada where ID_Utilizador = @UtilizadorExiste

		if @Permite_EntradaExiste > 0
		begin
			update Permite_Entrada set NoParque = @NoParque where ID = @Permite_EntradaExiste
			select 'true' as Resultado
		end
		else
		begin
			select 'false' as Resultado
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spPermiteEntrada_EntradaValida]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spPermiteEntrada_EntradaValida]
	@Login Varchar(10)
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		declare @Permite_EntradaExiste int

		select @Permite_EntradaExiste = ID from Permite_Entrada where ID_Utilizador = @UtilizadorExiste

		if @Permite_EntradaExiste > 0
		begin
			declare @currentDate date = CONVERT(date, GETDATE());
			declare @Pago_Desde date;
			declare @Pago_Ate date;
			declare @ParqueGratis bit;

			select @Pago_Desde = Pago_Desde, @Pago_Ate = Pago_Ate, @ParqueGratis = ParqueGratis from Permite_Entrada where ID_Utilizador = @Permite_EntradaExiste

			if (@currentDate >= @Pago_Desde and @currentDate <= @Pago_Ate) or (@ParqueGratis = 1)
			begin
				select 'true' as Resultado
			end
			else
			begin
				select 'false' as Resultado
			end
		end
		else
		begin
			select 'false' as Resultado
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spPermiteEntrada_Info]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spPermiteEntrada_Info]
	@Login Varchar(10)
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		declare @Permite_EntradaExiste int

		select @Permite_EntradaExiste = ID from Permite_Entrada where ID_Utilizador = @UtilizadorExiste

		if @Permite_EntradaExiste > 0
		begin
			select Pago_Desde, Pago_Ate, ParqueGratis from Permite_Entrada where ID = @Permite_EntradaExiste
		end
		else
		begin
			select Pago_Desde = NULL, Pago_Ate = NULL, ParqueGratis = 0
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end

select Pago_Desde, Pago_Ate, ParqueGratis from Permite_Entrada where ID_Utilizador = 6
GO
/****** Object:  StoredProcedure [dbo].[spPermiteEntrada_Inserir]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spPermiteEntrada_Inserir]
	@Login Varchar(10),
	@Pago_Desde date,
	@Pago_Ate date,
	@ParqueGratis bit
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		declare @PermiteEntradaExiste int

		select @PermiteEntradaExiste = ID from Permite_Entrada where ID_Utilizador = @UtilizadorExiste

		if @PermiteEntradaExiste > 0
		begin
			select 'false' as Resultado
		end
		else
		begin
			insert into Permite_Entrada(ID_Utilizador, Pago_Desde, Pago_Ate, NoParque, ParqueGratis) values (@UtilizadorExiste, @Pago_Desde, @Pago_Ate, 0, @ParqueGratis)
			select 'true' as Resultado
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spPermiteEntrada_JaEstaNoParque]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spPermiteEntrada_JaEstaNoParque]
	@Login Varchar(10)
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		declare @Permite_EntradaExiste int

		select @Permite_EntradaExiste = ID from Permite_Entrada where ID_Utilizador = @UtilizadorExiste

		if @Permite_EntradaExiste > 0
		begin
			declare @NoParque bit
			
			select @NoParque = NoParque From Permite_Entrada where ID_Utilizador = @UtilizadorExiste
			
			if @NoParque > 0
			begin
				select 'true' as Resultado
			end
			else
			begin
				select 'false' as Resultado
			end
		end
		else
		begin
			select 'false' as Resultado
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spRegistos_Entrada]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spRegistos_Entrada]
	@Login Varchar(10),
	@Matricula varchar(8)
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		declare @MatriculaExiste int

		select @MatriculaExiste = ID from Matriculas where ID_Utilizador = @UtilizadorExiste and Matricula = @Matricula

		if @MatriculaExiste > 0
		begin
			insert into Registos (ID_Matricula, Entrada, Saida) values (@MatriculaExiste, GETDATE(), '1900-01-01 00:00:00.000')
			exec spPermiteEntrada_AlterarEntradaNoParque @Login, 1
			select 'true' as Resultado
		end
		else
		begin
			select 'false' as Resultado
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spRegistos_Saida]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spRegistos_Saida]
	@Login Varchar(10),
	@Matricula varchar(8)
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		declare @MatriculaExiste int

		select @MatriculaExiste = ID from Matriculas where ID_Utilizador = @UtilizadorExiste and Matricula = @Matricula

		if @MatriculaExiste > 0
		begin
			declare @RegistoExiste int

			select @RegistoExiste = ID from Registos where ID_Matricula = @MatriculaExiste and Saida = '1900-01-01 00:00:00.000'

			if @RegistoExiste >0
			begin
				update Registos set Saida = GETDATE() where ID = @RegistoExiste
				exec spPermiteEntrada_AlterarEntradaNoParque @Login, 0
				select 'true' as Resultado
			end
			else
			begin
				select 'false' as Resultado
			end
		end
		else
		begin
			select 'false' as Resultado
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spUtilizadores_Alterar]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUtilizadores_Alterar]
	@Tipo_Utilizador Varchar(15),
	@Nome varchar (100),
	@Login Varchar(10),
	@ContaAtivada bit
as
begin
	declare @ID_Tipo_Utilizador int;

	select @ID_Tipo_Utilizador = ID from Tipo_Utilizadores where Tipo_Utilizador = @Tipo_Utilizador

	declare @UtilizadorExiste int;

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		update Utilizadores set ID_Tipo_Utilizador = @ID_Tipo_Utilizador, Nome = @Nome, Login = @Login, ContaAtivada = @ContaAtivada where ID = @UtilizadorExiste
		select 'true' as Resultado
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spUtilizadores_AlterarEstadoConta]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUtilizadores_AlterarEstadoConta]
	@Login Varchar(10),
	@ContaAtivada bit
as
begin
	declare @UtilizadorExiste int;

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		update Utilizadores set ContaAtivada = @ContaAtivada where ID = @UtilizadorExiste
		select 'true' as Resultado
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spUtilizadores_AlterarPassword]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUtilizadores_AlterarPassword]
	@Login Varchar(10),
	@Password varchar (64)
as
begin
	declare @UtilizadorExiste int;

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		update Utilizadores set Password = @Password where ID = @UtilizadorExiste
		select 'true' as Resultado
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spUtilizadores_ContaAtivada]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUtilizadores_ContaAtivada]
	@Login Varchar(10)
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores where Login = @Login

	if @UtilizadorExiste > 0
	begin
		declare @ContaAtivada bit

		select @ContaAtivada = ContaAtivada from Utilizadores where ID = @UtilizadorExiste

		if @ContaAtivada > 0
		begin
			select 'true' as Resultado
		end
		else
		begin
			select 'false' as Resultado
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spUtilizadores_GetTipoUtilizador]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUtilizadores_GetTipoUtilizador]
	@Login Varchar(10)
as
begin
	declare @UtilizadorExiste int

	select @UtilizadorExiste = ID from Utilizadores where Login = @Login

	if @UtilizadorExiste > 0
	begin
		select Tipo_Utilizador as Resultado from Utilizadores u, Tipo_Utilizadores tu where u.ID_Tipo_Utilizador = tu.ID and u.ID = @UtilizadorExiste
	end
	else
	begin
		select '' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spUtilizadores_Inserir]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUtilizadores_Inserir]
	@Tipo_Utilizador Varchar(15),
	@Nome varchar (100),
	@Login Varchar(10),
	@Password varchar (64),
	@ContaAtivada bit
as
begin
	declare @ID_Tipo_Utilizador int;

	select @ID_Tipo_Utilizador = ID from Tipo_Utilizadores where Tipo_Utilizador = @Tipo_Utilizador

	if @ID_Tipo_Utilizador > 0
	begin
		declare @UtilizadorExiste int;

		select @UtilizadorExiste = ID from Utilizadores	where Nome = @Nome and Login = @Login;

		if @UtilizadorExiste > 0
		begin
			select 'false' as Resultado
		end
		else
		begin
			insert into Utilizadores (ID_Tipo_Utilizador, Nome, Login, Password, ContaAtivada) values (@ID_Tipo_Utilizador, @Nome, @Login, @Password, @ContaAtivada)
			select 'true' as Resultado
		end
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spUtilizadores_ListarTodos]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUtilizadores_ListarTodos]
as
begin
	select Nome, Login, ContaAtivada, Tipo_Utilizador from Utilizadores, Tipo_Utilizadores where ID_Tipo_Utilizador = Tipo_Utilizadores.ID
end
GO
/****** Object:  StoredProcedure [dbo].[spUtilizadores_LoginValido]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUtilizadores_LoginValido]
	@Login Varchar(10),
	@Password varchar (64)
as
begin
	declare @LoginValido int;

	select @LoginValido = ID from Utilizadores where Login = @Login and Password = @Password;

	if @LoginValido > 0
	begin
		select 'true' as Resultado
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spUtilizadores_Procurar]    Script Date: 22/07/2022 00:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUtilizadores_Procurar]
	@Login Varchar(10)
as
begin
	declare @UtilizadorExiste int;

	select @UtilizadorExiste = ID from Utilizadores	where Login = @Login;

	if @UtilizadorExiste > 0
	begin
		select Nome, Login, ContaAtivada, Tipo_Utilizador from Utilizadores, Tipo_Utilizadores where Utilizadores.ID_Tipo_Utilizador = Tipo_Utilizadores.ID and Utilizadores.ID = @UtilizadorExiste
		select 'true' as Resultado
	end
	else
	begin
		select 'false' as Resultado
	end
end
GO
