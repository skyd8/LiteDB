using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LiteDB.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test1...\n");

            string filename = Path.Combine(Path.GetTempPath(), "temp.db");
            using (var db = new LiteDatabase(filename))
            {
                try
                {
                    BsonMapper.Global.Entity<ISystem>().Id(x => x.Id);
                    BsonMapper.Global.Entity<ProjectA>().Id(x => x.Id).DbRef(x => x.Systems);

                    SystemA sysA = new SystemA() { Name = "SystemA", SysDefine = "SystemA Define", SysMode = "A mode" };
                    SystemB sysB = new SystemB() { Name = "SystemB", SysDefine = "system B define", SysMode = "B mode", ItemsList = new List<object>() { 123 } };

                    ProjectA proj = new ProjectA() { Name = "Project1", PorjectType = "ProjectType", Systems = new List<ISystem>() { sysA, sysB } };
                    var proCol = db.GetCollection<ProjectA>("pros");
                    var sysCol = db.GetCollection<ISystem>("systems");

                    proCol.Insert(proj);
                    sysCol.InsertBulk(proj.Systems);

                    var projects = proCol.Include(x => x.Systems).FindAll();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"test1 exception: {ex.ToString()}");
                }
                finally
                {
                    db.DropCollection("pros");
                    db.DropCollection("systems");
                }
            }
            Console.WriteLine("\n");
            Console.WriteLine("test2...\n");
            using (var db = new LiteDatabase(filename))
            {
                try
                {
                    BsonMapper.Global.Entity<ISystem2>().Id(x => x.SysGuid);
                    BsonMapper.Global.Entity<SystemA2>().Id(x => x.SysGuid).Ignore(x => x.Id);
                    BsonMapper.Global.Entity<SystemB2>().Id(x => x.SysGuid).Ignore(x => x.Id);
                    BsonMapper.Global.Entity<ProjectA2>().Id(x => x.Id).DbRef(x => x.Systems);

                    SystemA2 sysA2 = new SystemA2() { Name = "SystemA", SysDefine = "SystemA Define", SysMode = "A mode" };
                    SystemB2 sysB2 = new SystemB2() { Name = "SystemB", SysDefine = "system B define", SysMode = "B mode", ItemsList = new List<object>() { 123 } };

                    ProjectA2 proj2 = new ProjectA2() { Name = "Project1", PorjectType = "ProjectType", Systems = new List<ISystem2>() { sysA2, sysB2 } };
                    var proCol = db.GetCollection<ProjectA2>("pros");
                    var sysCol = db.GetCollection<ISystem2>("systems");

                    proCol.Insert(proj2);
                    sysCol.InsertBulk(proj2.Systems);

                    var projects = proCol.Include(x => x.Systems).FindAll().ToList();
                    var count = projects[0].Systems.Count;
                    if(count !=2) 
                    Console.WriteLine($"Error: The Systems count = {count}, but it should be 2");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"test2 exception: {ex.ToString()}");
                }
                finally
                {
                    db.DropCollection("pros");
                    db.DropCollection("systems");
                }
            }
            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}