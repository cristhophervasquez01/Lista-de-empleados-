namespace ProyectoTarea.Models;

public class Empleado {
  public required int Id { get; set; }
  public required string Nombre { get; set; }
  public required string Cargo { get; set; }
  public required string Departamento { get; set; }
  public required string Horario { get; set; }
}