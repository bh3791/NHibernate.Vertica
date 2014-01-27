using FluentNHibernate.Mapping;

namespace NHibernate.Vertica.TestConsole
{
    public class CarMap : ClassMap<Car>
    {
        public CarMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned(); // Vertica doesn't have a way to return an sequence-generated identity
            Map(x => x.Title);
            Map(x => x.Description);
            References(x => x.Make).Column("MakeId");
            References(x => x.Model).Column("ModelId");
            Table("Car");
        }
    }

    public class MakeMap : ClassMap<Make>
    {
        public MakeMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Name);
            HasMany(x => x.Models)
                .KeyColumn("MakeId");
            Table("Make");
        }
    }

   public class ModelMap : ClassMap<Model>
   {
       public ModelMap()
       {
           Id(x => x.Id).GeneratedBy.Assigned();
           Map(x => x.Name);
           References(x => x.Make)
               .Column("MakeId");
           Table("Model");
       }
   }
}