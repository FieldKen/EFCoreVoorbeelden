using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

Console.WriteLine("Hello, World!");

using (ApplicationDbContext ctx = new ApplicationDbContext())
{
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
    foreach (var s in ctx.Students.OrderByDescending(s => s.Age))
    {
        Console.WriteLine($"{s.Name} - {s.Age} - {s.Address}");
    }

    int idUitCombobox = 1;
    Student? student = ctx.Students.FirstOrDefault(s => s.Id == idUitCombobox);
                                                   //IK WIL EEN STUDENT SELECTEREN
                                                     //WAARVAN
                                                        //DE STUDENT ZIJN ID
                                                             //HETZELFDE IS DAN
                                                                //HET ID UIT DE COMBOBOX

    Console.WriteLine(student.Name);

    //UPDATE
    int idVanArno = 2;
    Student st = ctx.Students.FirstOrDefault(s => s.Id == idVanArno);
    st.Name = "Emlyn";
    ctx.SaveChanges();

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

    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }
}


public class Student
{
    public int Id { get; set; }
    [MaxLength(50)] public string Name { get; set; }
    public int? Age { get; set; }
    public string Address { get; set; }

    public Student()
    {

    }

    public Student(string name, int age, string address)
    {
        Name = name;
        Age = age;
        Address = address;
    }
}

public class Grade
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
}