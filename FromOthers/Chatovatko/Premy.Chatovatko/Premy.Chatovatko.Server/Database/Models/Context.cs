using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Premy.Chatovatko.Server.Database.Models
{
    public partial class Context : DbContext
    {
        private ServerConfig config = null;
        public Context(ServerConfig config)
        {
            this.config = config;
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<BlobMessages> BlobMessages { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<UsersKeys> UsersKeys { get; set; }
        public virtual DbSet<ClientsMessagesDownloaded> ClientsMessagesDownloaded { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseMySql(config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlobMessages>(entity =>
            {
                entity.ToTable("blob_messages");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SenderId)
                    .HasName("sender_id");

                entity.HasIndex(e => new { e.RecepientId, e.Id })
                    .HasName("recepiant_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("blob");

                entity.Property(e => e.RecepientId)
                    .HasColumnName("recepient_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SenderId)
                    .HasColumnName("sender_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Priority)
                    .HasColumnName("priority")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Recepient)
                    .WithMany(p => p.BlobMessagesRecepient)
                    .HasForeignKey(d => d.RecepientId)
                    .HasConstraintName("recepient_id_foreign_key");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.BlobMessagesSender)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("sender_id_foreign_key");
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.ToTable("clients");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_clients_users1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_clients_users1");
            });

            modelBuilder.Entity<ClientsMessagesDownloaded>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ClientsId, e.BlobMessagesId });

                entity.ToTable("clients_messages_downloaded");

                entity.HasIndex(e => e.BlobMessagesId)
                    .HasName("fk_clients_messages_downloaded_blob_messages1_idx");

                entity.HasIndex(e => e.ClientsId)
                    .HasName("fk_clients_messages_downloaded_clients1_idx");

                entity.HasIndex(e => new { e.ClientsId, e.BlobMessagesId })
                    .HasName("client_messages_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientsId)
                    .HasColumnName("clients_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BlobMessagesId)
                    .HasColumnName("blob_messages_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.BlobMessages)
                    .WithMany(p => p.ClientsMessagesDownloaded)
                    .HasForeignKey(d => d.BlobMessagesId)
                    .HasConstraintName("fk_clients_messages_downloaded_blob_messages1");

                entity.HasOne(d => d.Clients)
                    .WithMany(p => p.ClientsMessagesDownloaded)
                    .HasForeignKey(d => d.ClientsId)
                    .HasConstraintName("fk_clients_messages_downloaded_clients1");
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.ToTable("logs");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasColumnName("class")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Error)
                    .HasColumnName("error")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.TimeOfCreation)
                    .HasColumnName("time_of_creation")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PublicCertificateSha256)
                    .HasName("public_certificate_sha256_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("user_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PublicCertificate)
                    .IsRequired()
                    .HasColumnName("public_certificate")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.PublicCertificateSha256)
                    .IsRequired()
                    .HasColumnName("public_certificate_sha256")
                    .HasMaxLength(64);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<UsersKeys>(entity =>
            {
                entity.ToTable("users_keys");

                entity.HasIndex(e => e.SenderId)
                    .HasName("fk_public_certificates_users2_idx");

                entity.HasIndex(e => new { e.RecepientId, e.SenderId })
                    .HasName("user_id_keys")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AesKey)
                    .IsRequired()
                    .HasColumnName("aes_key")
                    .HasMaxLength(1024);

                entity.Property(e => e.RecepientId)
                    .HasColumnName("recepient_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SenderId)
                    .HasColumnName("sender_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Trusted)
                    .HasColumnName("trusted")
                    .HasColumnType("bit(1)");

                entity.HasOne(d => d.Recepient)
                    .WithMany(p => p.UsersKeysRecepient)
                    .HasForeignKey(d => d.RecepientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_public_certificates_users1");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.UsersKeysSender)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_public_certificates_users2");
            });
        }
    }
}
