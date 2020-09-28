using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Premy.Chatovatko.Client.Libs.UserData;

namespace Premy.Chatovatko.Client.Libs.Database.Models
{
    public partial class Context : DbContext
    {
        IClientDatabaseConfig config = null;
        public Context(IClientDatabaseConfig config)
        {
            this.config = config;
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Alarms> Alarms { get; set; }
        public virtual DbSet<BlobMessages> BlobMessages { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<MessagesThread> MessagesThread { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<ToSendMessages> ToSendMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseSqlite(String.Format("Data Source={0}", config.DatabaseAddress));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alarms>(entity =>
            {
                entity.ToTable("alarms");

                entity.HasIndex(e => e.BlobMessagesId)
                    .HasName("fk_alarms_blob_messages_id_idx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BlobMessagesId)
                    .HasColumnName("blob_messages_id")
                    .HasColumnType("INT");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasColumnType("MEDIUMTEXT");

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasColumnName("time")
                    .HasColumnType("DATETIME");

                entity.HasOne(d => d.BlobMessages)
                    .WithOne(p => p.Alarms)
                    .HasForeignKey<Alarms>(d => d.BlobMessagesId);
            });

            modelBuilder.Entity<BlobMessages>(entity =>
            {
                entity.ToTable("blob_messages");

                entity.HasIndex(e => e.PublicId)
                    .HasName("fk_blob_messages_public_id_idx")
                    .IsUnique();

                entity.HasIndex(e => e.SenderId)
                    .HasName("fk_blob_messages_contacts2_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DoDelete)
                    .HasColumnName("do_delete")
                    .HasColumnType("TINYINT");

                entity.Property(e => e.Failed)
                    .HasColumnName("failed")
                    .HasColumnType("TINYINT");

                entity.Property(e => e.PublicId)
                    .HasColumnName("public_id")
                    .HasColumnType("INT");

                entity.Property(e => e.SenderId)
                    .HasColumnName("sender_id")
                    .HasColumnType("INT");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.BlobMessages)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.HasKey(e => e.PublicId);

                entity.ToTable("contacts");

                entity.HasIndex(e => e.BlobMessagesId)
                    .HasName("fk_contact_blob_messages_id_idx")
                    .IsUnique();

                entity.Property(e => e.PublicId)
                    .HasColumnName("public_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AlarmPermission)
                    .HasColumnName("alarm_permission")
                    .HasColumnType("TINYINT");

                entity.Property(e => e.BlobMessagesId)
                    .HasColumnName("blob_messages_id")
                    .HasColumnType("INT");

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasColumnName("nick_name")
                    .HasColumnType("VARCHAR");

                entity.Property(e => e.PublicCertificate)
                    .IsRequired()
                    .HasColumnName("public_certificate")
                    .HasColumnType("VARCHAR");

                entity.Property(e => e.ReceiveAesKey).HasColumnName("receive_aes_key");

                entity.Property(e => e.SendAesKey).HasColumnName("send_aes_key");

                entity.Property(e => e.Trusted)
                    .HasColumnName("trusted")
                    .HasColumnType("TINYINT");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasColumnType("VARCHAR");

                entity.HasOne(d => d.BlobMessagesNavigation)
                    .WithOne(p => p.Contacts)
                    .HasForeignKey<Contacts>(d => d.BlobMessagesId);
            });

           
            modelBuilder.Entity<Messages>(entity =>
            {
                entity.ToTable("messages");

                entity.HasIndex(e => e.BlobMessagesId)
                    .HasName("fk_messages_blob_messages1_idx")
                    .IsUnique();

                entity.HasIndex(e => e.IdMessagesThread)
                    .HasName("fk_messages_messages_thread1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BlobMessagesId)
                    .HasColumnName("blob_messages_id")
                    .HasColumnType("INT");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnName("date")
                    .HasColumnType("DATETIME");

                entity.Property(e => e.IdMessagesThread)
                    .HasColumnName("id_messages_thread")
                    .HasColumnType("INT");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasColumnType("MEDIUMTEXT");

                entity.Property(e => e.Attechment)
                    .HasColumnName("attechment")
                    .HasColumnType("BLOB");

                entity.HasOne(d => d.BlobMessages)
                    .WithOne(p => p.Messages)
                    .HasForeignKey<Messages>(d => d.BlobMessagesId);

                entity.HasOne(d => d.IdMessagesThreadNavigation)
                    .WithMany(p => p.Messages)
                    .HasPrincipalKey(p => p.PublicId)
                    .HasForeignKey(d => d.IdMessagesThread)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MessagesThread>(entity =>
            {
                entity.ToTable("messages_thread");

                entity.HasIndex(e => e.BlobMessagesId)
                    .HasName("fk_mess_thr_blob_mess_id_idx")
                    .IsUnique();

                entity.HasIndex(e => e.PublicId)
                    .HasName("fk_mess_thr_public_id_idx")
                    .IsUnique();

                entity.HasIndex(e => new { e.WithUser, e.Name })
                    .HasName("fk_mess_thr_with_user_id_idx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Archived)
                    .HasColumnName("archived")
                    .HasColumnType("TINYINT");

                entity.Property(e => e.BlobMessagesId)
                    .HasColumnName("blob_messages_id")
                    .HasColumnType("INT");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR");

                entity.Property(e => e.Onlive)
                    .HasColumnName("onlive")
                    .HasColumnType("TINYINT");

                entity.Property(e => e.PublicId)
                    .HasColumnName("public_id")
                    .HasColumnType("BIGINT");

                entity.Property(e => e.WithUser)
                    .HasColumnName("with_user")
                    .HasColumnType("INT");

                entity.HasOne(d => d.BlobMessages)
                    .WithOne(p => p.MessagesThread)
                    .HasForeignKey<MessagesThread>(d => d.BlobMessagesId);

                entity.HasOne(d => d.WithUserNavigation)
                    .WithMany(p => p.MessagesThread)
                    .HasForeignKey(d => d.WithUser)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.ToTable("settings");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.PrivateCertificate)
                    .IsRequired()
                    .HasColumnName("private_certificate")
                    .HasColumnType("VARCHAR");

                entity.Property(e => e.ServerAddress)
                    .IsRequired()
                    .HasColumnName("server_address")
                    .HasColumnType("VARCHAR");

                entity.Property(e => e.ServerName)
                    .IsRequired()
                    .HasColumnName("server_name")
                    .HasColumnType("VARCHAR");

                entity.Property(e => e.ServerPublicCertificate)
                    .IsRequired()
                    .HasColumnName("server_public_certificate")
                    .HasColumnType("VARCHAR");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasColumnType("VARCHAR");

                entity.Property(e => e.UserPublicId)
                    .IsRequired()
                    .HasColumnName("user_public_id")
                    .HasColumnType("INT");

                entity.Property(e => e.LastUniqueId)
                    .IsRequired()
                    .HasColumnName("last_unique_id")
                    .HasColumnType("BIGINT");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasColumnName("client_id")
                    .HasColumnType("INT");
            });

            modelBuilder.Entity<ToSendMessages>(entity =>
            {
                entity.ToTable("to_send_messages");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Blob)
                    .IsRequired()
                    .HasColumnName("blob");

                entity.Property(e => e.BlobMessagesId)
                    .HasColumnName("blob_messages_id")
                    .HasColumnType("INT");

                entity.Property(e => e.RecepientId)
                    .HasColumnName("recepient_id")
                    .HasColumnType("INT");

                entity.Property(e => e.Priority)
                    .HasColumnName("priority")
                    .HasColumnType("INT");

                entity.HasOne(d => d.BlobMessages)
                    .WithMany(p => p.ToSendMessages)
                    .HasForeignKey(d => d.BlobMessagesId);

                entity.HasOne(d => d.Recepient)
                    .WithMany(p => p.ToSendMessages)
                    .HasForeignKey(d => d.RecepientId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
