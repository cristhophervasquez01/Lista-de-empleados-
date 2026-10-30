using ProyectoTarea.Models;

namespace ProyectoTarea.Services;

public class GestorService {
  private readonly List<Empleado> _empleados = [
    new Empleado {
      Id = 1,
      Nombre = "María Pérez",
      Cargo = "Gerente",
      Departamento = "Administración",
      Horario = "07:00 am - 03:00 pm"
    },

    new Empleado {
      Id = 2,
      Nombre = "Juan Rodríguez",
      Cargo = "Desarrollador",
      Departamento = "Tecnología",
      Horario = "09:00 am - 05:00 pm"
    },
  ];

  public List<Empleado> FindAll() {
    return _empleados;
  }

  public bool Delete(int id) {
    var empleado = _empleados.FirstOrDefault((e) => e.Id == id);

    if (empleado is null) {
      return false;
    }

    _empleados.Remove(empleado);
    return true;
  }

  public bool Create(Empleado empleado) {
    _empleados.Add(empleado);
    return true;
  }
}