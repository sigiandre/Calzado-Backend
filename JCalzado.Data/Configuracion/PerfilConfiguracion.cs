using JCalzado.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCalzado.Data.Configuracion
{
    public class PerfilConfiguracion : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> entity)
        {
            entity.ToTable("Perfil");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        }
    }
}
