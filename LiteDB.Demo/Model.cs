using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDB.Demo
{
    public interface IBase
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }

    #region for test1
    public interface IProject : IBase
    {
        string PorjectType { get; set; }
        List<ISystem> Systems { get; set; }
    }

    public interface ISystem : IBase
    {
        string SysDefine { get; set; }
        string SysMode { get; set; }
    }

    public class ProjectA : IProject
    {
        public ProjectA()
        {
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public string PorjectType { get; set; }
        public List<ISystem> Systems { get; set; }
    }


    public class SystemA : ISystem
    {
        public SystemA()
        {
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public string SysDefine { get; set; }
        public string SysMode { get; set; }
    }

    public class SystemB : ISystem
    {
        public SystemB()
        {
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public string SysDefine { get; set; }
        public string SysMode { get; set; }

        public List<object> ItemsList { get; set; }
    }

    #endregion

    #region for test2
    public interface IProject2 : IBase
    {
        string PorjectType { get; set; }
        List<ISystem2> Systems { get; set; }
    }


    public interface ISystem2 : IBase
    {
        Guid SysGuid { get; set; }
        string SysDefine { get; set; }
        string SysMode { get; set; }
    }

    public class ProjectA2 : IProject2
    {
        public ProjectA2()
        {
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public string PorjectType { get; set; }
        public List<ISystem2> Systems { get; set; }
    }


    public class SystemA2 : ISystem2
    {
        public SystemA2()
        {
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SysGuid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public string SysDefine { get; set; }
        public string SysMode { get; set; }
    }

    public class SystemB2 : ISystem2
    {
        public SystemB2()
        {
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SysGuid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public string SysDefine { get; set; }
        public string SysMode { get; set; }

        public List<object> ItemsList { get; set; }
    }
    #endregion

}
