namespace LoggingDemo.SerilogDemo;

public class Person
{
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
    
    public string Name { get; init; }
    public int Age { get; init; }
}