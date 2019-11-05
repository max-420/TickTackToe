using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Configurations
{
	public class StepConfiguration : IEntityTypeConfiguration<Step>
	{
		public void Configure(EntityTypeBuilder<Step> builder)
		{
			builder.HasKey(x => x.Id);
		}
	}
}
