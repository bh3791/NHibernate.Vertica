using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernate.Vertica.TestConsole
{
    public class Car
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual Make Make { get; set; }
        public virtual Model Model { get; set; }
    }

    public class Make
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Model> Models { get; set; }
    }

    public class Model
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Make Make { get; set; }
    }
}
