using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Core
{
    public partial class AgendeMeContext : DbContext
    {
        public AgendeMeContext()
        {
        }

        public AgendeMeContext(DbContextOptions<AgendeMeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgendaDoServico> Agendadoservicos { get; set; }
        public virtual DbSet<Agendamento> Agendamentos { get; set; }
        public virtual DbSet<Areadeservico> Areadeservicos { get; set; }
        public virtual DbSet<Atendenteorgaopublico> Atendenteorgaopublicos { get; set; }
        public virtual DbSet<Cargo> Cargos { get; set; }
        public virtual DbSet<Cidadao> Cidadaos { get; set; }
        public virtual DbSet<Orgaopublico> Orgaopublicos { get; set; }
        public virtual DbSet<Prefeitura> Prefeituras { get; set; }
        public virtual DbSet<Profissionalcargo> Profissionalcargos { get; set; }
        public virtual DbSet<Profissionalprefeitura> Profissionalprefeituras { get; set; }
        public virtual DbSet<Servicopublico> Servicopublicos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=123456;database=AgendeMe");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgendaDoServico>(entity =>
            {
                entity.ToTable("agendadoservico");

                entity.HasIndex(e => e.IdProfissional, "fk_Agenda_Cidadao1_idx");

                entity.HasIndex(e => e.IdServicoPublico, "fk_Agenda_ServicoPublico1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.DiaSemana)
                    .IsRequired()
                    .HasColumnType("enum('Segunda','Terça','Quarta','Quinta','Sexta')")
                    .HasColumnName("diaSemana");

                entity.Property(e => e.HorarioFim)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("horarioFim")
                    .IsFixedLength(true);

                entity.Property(e => e.HorarioInicio)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("horarioInicio")
                    .IsFixedLength(true);

                entity.Property(e => e.IdProfissional)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idProfissional");

                entity.Property(e => e.IdServicoPublico)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idServicoPublico");

                entity.Property(e => e.VagasAtendimento)
                    .HasColumnType("int unsigned")
                    .HasColumnName("vagasAtendimento");

                entity.Property(e => e.VagasRetorno)
                    .HasColumnType("int unsigned")
                    .HasColumnName("vagasRetorno");

                entity.HasOne(d => d.IdProfissionalNavigation)
                    .WithMany(p => p.Agendadoservicos)
                    .HasForeignKey(d => d.IdProfissional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Agenda_Cidadao1");

                entity.HasOne(d => d.IdServicoPublicoNavigation)
                    .WithMany(p => p.Agendadoservicos)
                    .HasForeignKey(d => d.IdServicoPublico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Agenda_ServicoPublico1");
            });

            modelBuilder.Entity<Agendamento>(entity =>
            {
                entity.ToTable("agendamento");

                entity.HasIndex(e => e.Data, "dataAgendamento");

                entity.HasIndex(e => e.DataCadastro, "dataAtendimento");

                entity.HasIndex(e => e.IdRetorno, "fk_Agendamento_Agendamento1_idx");

                entity.HasIndex(e => e.IdAtendente, "fk_Agendamento_Atendente_idx");

                entity.HasIndex(e => e.IdProfissional, "fk_Agendamento_Cidadao3_idx");

                entity.HasIndex(e => e.IdCidadao, "fk_Agendamento_Cidadao_idx");

                entity.HasIndex(e => e.IdServicoPublico, "fk_Agendamento_ServicoPublico1_idx");

                entity.HasIndex(e => e.Situacao, "situacao");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.DataCadastro).HasColumnName("dataCadastro");

                entity.Property(e => e.HorarioFim)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("horarioFim")
                    .IsFixedLength(true);

                entity.Property(e => e.HorarioInicio)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("horarioInicio")
                    .IsFixedLength(true);

                entity.Property(e => e.IdAtendente)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idAtendente");

                entity.Property(e => e.IdCidadao)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idCidadao");

                entity.Property(e => e.IdProfissional)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idProfissional");

                entity.Property(e => e.IdRetorno)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idRetorno");

                entity.Property(e => e.IdServicoPublico)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idServicoPublico");

                entity.Property(e => e.Situacao)
                    .IsRequired()
                    .HasColumnType("enum('Agendado','Cancelado','Remarcado','Aguardando Atendimento','Atendido')")
                    .HasColumnName("situacao");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("enum('Agendamento','Retorno')")
                    .HasColumnName("tipo");

                entity.HasOne(d => d.IdAtendenteNavigation)
                    .WithMany(p => p.AgendamentoIdAtendenteNavigations)
                    .HasForeignKey(d => d.IdAtendente)
                    .HasConstraintName("fk_Agendamento_Cidadao2");

                entity.HasOne(d => d.IdCidadaoNavigation)
                    .WithMany(p => p.AgendamentoIdCidadaoNavigations)
                    .HasForeignKey(d => d.IdCidadao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Agendamento_Cidadao1");

                entity.HasOne(d => d.IdProfissionalNavigation)
                    .WithMany(p => p.AgendamentoIdProfissionalNavigations)
                    .HasForeignKey(d => d.IdProfissional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Agendamento_Cidadao3");

                entity.HasOne(d => d.IdRetornoNavigation)
                    .WithMany(p => p.InverseIdRetornoNavigation)
                    .HasForeignKey(d => d.IdRetorno)
                    .HasConstraintName("fk_Agendamento_Agendamento1");

                entity.HasOne(d => d.IdServicoPublicoNavigation)
                    .WithMany(p => p.Agendamentos)
                    .HasForeignKey(d => d.IdServicoPublico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Agendamento_ServicoPublico1");
            });

            modelBuilder.Entity<Areadeservico>(entity =>
            {
                entity.ToTable("areadeservico");

                entity.HasIndex(e => e.IdPrefeitura, "fk_Area_Prefeitura1_idx");

                entity.HasIndex(e => e.Nome, "nome");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Icone)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("icone");

                entity.Property(e => e.IdPrefeitura)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idPrefeitura");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("nome");

                entity.HasOne(d => d.IdPrefeituraNavigation)
                    .WithMany(p => p.Areadeservicos)
                    .HasForeignKey(d => d.IdPrefeitura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Area_Prefeitura1");
            });

            modelBuilder.Entity<Atendenteorgaopublico>(entity =>
            {
                entity.HasKey(e => new { e.IdAtendente, e.IdOrgaoPublico })
                    .HasName("PRIMARY");

                entity.ToTable("atendenteorgaopublico");

                entity.HasIndex(e => e.IdAtendente, "fk_CidadaoOrgaoPublico_Cidadao1_idx");

                entity.HasIndex(e => e.IdOrgaoPublico, "fk_CidadaoOrgaoPublico_OrgaoPublico1_idx");

                entity.Property(e => e.IdAtendente)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idAtendente");

                entity.Property(e => e.IdOrgaoPublico)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idOrgaoPublico");

                entity.HasOne(d => d.IdAtendenteNavigation)
                    .WithMany(p => p.Atendenteorgaopublicos)
                    .HasForeignKey(d => d.IdAtendente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CidadaoOrgaoPublico_Cidadao1");

                entity.HasOne(d => d.IdOrgaoPublicoNavigation)
                    .WithMany(p => p.Atendenteorgaopublicos)
                    .HasForeignKey(d => d.IdOrgaoPublico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CidadaoOrgaoPublico_OrgaoPublico1");
            });

            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.ToTable("cargo");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(150)
                    .HasColumnName("descricao");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Cidadao>(entity =>
            {
                entity.ToTable("cidadao");

                entity.HasIndex(e => e.Cpf, "cpf_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdOrgaoPublico, "fk_Cidadao_OrgaoPublico1_idx");

                entity.HasIndex(e => e.IdPrefeitura, "fk_Cidadao_Prefeitura1_idx");

                entity.HasIndex(e => e.Nome, "nome");

                entity.HasIndex(e => e.Sus, "sus_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("bairro");

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("cep");

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("cidade");

                entity.Property(e => e.Complemento)
                    .HasMaxLength(100)
                    .HasColumnName("complemento");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("cpf");

                entity.Property(e => e.DataNascimento)
                    .HasColumnType("date")
                    .HasColumnName("dataNascimento");

                entity.Property(e => e.Email)
                    .HasMaxLength(70)
                    .HasColumnName("email");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.IdOrgaoPublico)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idOrgaoPublico");

                entity.Property(e => e.IdPrefeitura)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idPrefeitura");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("nome");

                entity.Property(e => e.NumeroCasa)
                    .IsRequired()
                    .HasMaxLength(7)
                    .HasColumnName("numeroCasa");

                entity.Property(e => e.Rua)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("rua");

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("sexo")
                    .IsFixedLength(true);

                entity.Property(e => e.Sus)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("sus");

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("telefone");

                entity.Property(e => e.TipoCidadao)
                    .IsRequired()
                    .HasColumnType("enum('Administrador','Atendente','gestorOrgao','gestorPrefeitura','Profissional','Cidadao')")
                    .HasColumnName("tipoCidadao");

                entity.HasOne(d => d.IdOrgaoPublicoNavigation)
                    .WithMany(p => p.Cidadaos)
                    .HasForeignKey(d => d.IdOrgaoPublico)
                    .HasConstraintName("fk_Cidadao_OrgaoPublico1");

                entity.HasOne(d => d.IdPrefeituraNavigation)
                    .WithMany(p => p.Cidadaos)
                    .HasForeignKey(d => d.IdPrefeitura)
                    .HasConstraintName("fk_Cidadao_Prefeitura1");
            });

            modelBuilder.Entity<Orgaopublico>(entity =>
            {
                entity.ToTable("orgaopublico");

                entity.HasIndex(e => e.IdPrefeitura, "fk_OrgaoPublico_Prefeitura1_idx");

                entity.HasIndex(e => e.Nome, "nome");

                entity.HasIndex(e => e.Numero, "numero");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("bairro");

                entity.Property(e => e.Cep)
                    .HasMaxLength(10)
                    .HasColumnName("cep");

                entity.Property(e => e.Complemento)
                    .HasMaxLength(70)
                    .HasColumnName("complemento");

                entity.Property(e => e.HoraAbre)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("horaAbre")
                    .IsFixedLength(true);

                entity.Property(e => e.HoraFecha)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("horaFecha")
                    .IsFixedLength(true);

                entity.Property(e => e.IdPrefeitura)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idPrefeitura");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("nome");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(7)
                    .HasColumnName("numero");

                entity.Property(e => e.Rua)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("rua");

                entity.HasOne(d => d.IdPrefeituraNavigation)
                    .WithMany(p => p.Orgaopublicos)
                    .HasForeignKey(d => d.IdPrefeitura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_OrgaoPublico_Prefeitura1");
            });

            modelBuilder.Entity<Prefeitura>(entity =>
            {
                entity.ToTable("prefeitura");

                entity.HasIndex(e => e.Cnpj, "cnpj_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Nome, "nome");

                entity.HasIndex(e => e.Numero, "numero");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Bairro)
                    .HasMaxLength(70)
                    .HasColumnName("bairro");

                entity.Property(e => e.Cep)
                    .HasMaxLength(10)
                    .HasColumnName("cep");

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("cidade");

                entity.Property(e => e.Cnpj)
                    .HasMaxLength(20)
                    .HasColumnName("cnpj");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.Icone)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("icone");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("nome");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(7)
                    .HasColumnName("numero");

                entity.Property(e => e.Rua)
                    .HasMaxLength(70)
                    .HasColumnName("rua");
            });

            modelBuilder.Entity<Profissionalcargo>(entity =>
            {
                entity.HasKey(e => new { e.IdCargo, e.IdProfissional })
                    .HasName("PRIMARY");

                entity.ToTable("profissionalcargo");

                entity.HasIndex(e => e.IdCargo, "fk_CargoCidadao_Cargo1_idx");

                entity.HasIndex(e => e.IdProfissional, "fk_CargoCidadao_Cidadao1_idx");

                entity.Property(e => e.IdCargo)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idCargo");

                entity.Property(e => e.IdProfissional)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idProfissional");

                entity.HasOne(d => d.IdCargoNavigation)
                    .WithMany(p => p.Profissionalcargos)
                    .HasForeignKey(d => d.IdCargo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CargoCidadao_Cargo1");

                entity.HasOne(d => d.IdProfissionalNavigation)
                    .WithMany(p => p.Profissionalcargos)
                    .HasForeignKey(d => d.IdProfissional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CargoCidadao_Cidadao1");
            });

            modelBuilder.Entity<Profissionalprefeitura>(entity =>
            {
                entity.HasKey(e => new { e.IdProfissional, e.IdPrefeitura })
                    .HasName("PRIMARY");

                entity.ToTable("profissionalprefeitura");

                entity.HasIndex(e => e.IdProfissional, "fk_CidadaoPrefeitura_Cidadao1_idx");

                entity.HasIndex(e => e.IdPrefeitura, "fk_CidadaoPrefeitura_Prefeitura1_idx");

                entity.Property(e => e.IdProfissional)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idProfissional");

                entity.Property(e => e.IdPrefeitura)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idPrefeitura");

                entity.HasOne(d => d.IdPrefeituraNavigation)
                    .WithMany(p => p.Profissionalprefeituras)
                    .HasForeignKey(d => d.IdPrefeitura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CidadaoPrefeitura_Prefeitura1");

                entity.HasOne(d => d.IdProfissionalNavigation)
                    .WithMany(p => p.Profissionalprefeituras)
                    .HasForeignKey(d => d.IdProfissional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CidadaoPrefeitura_Cidadao1");
            });

            modelBuilder.Entity<Servicopublico>(entity =>
            {
                entity.ToTable("servicopublico");

                entity.HasIndex(e => e.IdArea, "fk_ServicoPublico_Area1_idx");

                entity.HasIndex(e => e.IdOrgaoPublico, "fk_ServicoPublico_OrgaoPublico1_idx");

                entity.HasIndex(e => e.Nome, "nome");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Icone)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("icone");

                entity.Property(e => e.IdArea).HasColumnName("idArea");

                entity.Property(e => e.IdOrgaoPublico)
                    .HasColumnType("int unsigned")
                    .HasColumnName("idOrgaoPublico");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("nome");

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.Servicopublicos)
                    .HasForeignKey(d => d.IdArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ServicoPublico_Area1");

                entity.HasOne(d => d.IdOrgaoPublicoNavigation)
                    .WithMany(p => p.Servicopublicos)
                    .HasForeignKey(d => d.IdOrgaoPublico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ServicoPublico_OrgaoPublico1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
