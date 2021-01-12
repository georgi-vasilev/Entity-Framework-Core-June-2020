namespace RecipeApp.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RecipeApp.Data.Models;

    public class RecipeConfigurations : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasIndex(x => x.Name);
            //builder.Ignore(x => x.Test);//this property will be hidden for the db
            builder.Property<string>("Egn") //the property will exist in the db however
                .HasColumnType("NVARCHAR(10)"); //it wont be able to be accessed with ef core

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired();
        }
    }
}
