using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

Console.WriteLine("Hello, World!");

using (ApplicationDbContext ctx = new ApplicationDbContext())
{
    /*ctx.BarBeer.Add(new BarBeer(1, 1));
    ctx.BarBeer.Add(new BarBeer(1, 2));
    ctx.BarBeer.Add(new BarBeer(1, 3));
    ctx.BarBeer.Add(new BarBeer(1, 4));
    ctx.BarBeer.Add(new BarBeer(2, 1));
    ctx.BarBeer.Add(new BarBeer(2, 5));*/

    var query = ctx.Bar.Join(ctx.BarBeer,           //JOIN BARBEER MET BAR
        bar => bar.Id,                              //ID VAN TABEL 1
        barbeer => barbeer.BarId,                   //ID VAN TABEL 2
        (bar, barbeer) => new { bar, barbeer })     //NIEUW "ANONYMOUS" OBJECT MAKEN
        .Join(ctx.Beer,                                     //JOIN TABEL BEER MET BARBARBEER (komende van { bar, barbeer }) 
        barbarbeer => barbarbeer.barbeer.BeerId,            //ID VAN TABEL 1 ({ bar, barbeer })
        beer => beer.Id,                                    //ID VAN TABEL 2
        (barbarbeer, beer) => new { barbarbeer, beer });    //NIEUW "ANONYMOUS" OBJECT MAKEN

    foreach (var item in query)
    {
        Console.WriteLine($"{item.barbarbeer.bar.Naam} - {item.beer.Naam}");
    }

    /*ctx.Grades.Add(new Grade() { Name = "Informaticabeheer", Year = 5 });
    ctx.Grades.Add(new Grade() { Name = "Informaticabeheer", Year = 6 });
    ctx.Grades.Add(new Grade() { Name = "Economie Talen", Year = 5 });
    ctx.Grades.Add(new Grade() { Name = "Economie Talen", Year = 6 });*/


    /*ctx.Students.Add(new Student("Ken", 26, "Stationstraat 1") { Grade = ctx.Grades.FirstOrDefault(g => g.Id == 9) });
    ctx.Students.Add(new Student("Bart", 22, "Stationstraat 2") { Grade = ctx.Grades.FirstOrDefault(g => g.Id == 9) });
    ctx.Students.Add(new Student("Gert", 25, "Stationstraat 3") { Grade = ctx.Grades.FirstOrDefault(g => g.Id == 10) });
    ctx.Students.Add(new Student("Tom", 27, "Stationstraat 4") { Grade = ctx.Grades.FirstOrDefault(g => g.Id == 10) });*/
    //ctx.Students.Add(new Student("Stefaan", 30, "Stationstraat 5") { Grade = ctx.Grades.Where(g=>g.Name == "Informaticabeheer").FirstOrDefault(g=>g.Year == 6) });


    //TOEVOEGEN
    /*ctx.Students.Add(new Student("Ken", 26, "Stationstraat 1"));
    ctx.Students.Add(new Student()
    {
        Name = "Arno",
        Age = 28,
        Address = "Stationstraat 2"
    });

    ctx.SaveChanges();*/

    //LEZEN
    /*foreach (var g in ctx.Grades)
    {
        Console.WriteLine($"{g.Name} {g.Year}");
    }

    foreach (var s in ctx.Students.OrderByDescending(s => s.Age))
    {
        Console.WriteLine($"{s.Name} - {s.Age} - {s.Address}");
    }

    int idUitCombobox = 1;
    Student? student = ctx.Students.FirstOrDefault(s => s.Id == idUitCombobox);

    Console.WriteLine(student.Name);*/

    /*foreach (var s in ctx.Students.Include(g => g.Grade))
    {
        Console.WriteLine($"{s.Name} - {s.Age} - {s.Address} - {s.Grade.Year} {s.Grade.Name}");
    }*/

    //UPDATE
    /*int idVanArno = 2;
    Student st = ctx.Students.FirstOrDefault(s => s.Id == idVanArno);
    st.Name = "Emlyn";
    ctx.SaveChanges();*/

    //REMOVE
    /*int idVanMathias = 3;
    Student st = ctx.Students.FirstOrDefault(s => s.Id == idVanMathias);
    ctx.Students.Remove(st);
    ctx.SaveChanges();*/
}

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ApplicationDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BarBeer>().HasKey(bb => new { bb.BeerId, bb.BarId });
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Bar> Bar { get; set; }
    public DbSet<Beer> Beer { get; set; }
    public DbSet<BarBeer> BarBeer { get; set; }

}

public class BarBeer
{
    public BarBeer(int barId, int beerId)
    {
        BarId = barId;
        BeerId = beerId;
    }

    //public int BarBeerId { get; set; }
    public int BarId { get; set; }
    public Bar Bar { get; set; } = new Bar();
    public int BeerId { get; set; }
    public Beer Beer { get; set; } = new Beer();
}

public class Bar
{
    public int Id { get; set; }
    public string Naam { get; set; }
    public ICollection<BarBeer> BarBeer { get; set; }
}

public class Beer
{
    public int Id { get; set; }
    public string Naam { get; set; }
    public ICollection<BarBeer> BarBeer { get; set; }
}


public class Student
{
    public int Id { get; set; }
    [MaxLength(50)] public string Name { get; set; }
    public int? Age { get; set; }
    public string Address { get; set; }
    public int GradeId { get; set; }
    public Grade Grade { get; set; } = new Grade();

    public Student()
    {

    }
    public Student(string name, int age, string address)
    {
        Name = name;
        Age = age;
        Address = address;
    }

    public override string ToString()
    {
        return $"{Name} - {Age} - {Address}";
    }
}

public class Grade
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public ICollection<Student> Students { get; set; }
}