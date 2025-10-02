using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace WebMVC.Controllers
{
    [Authorize]
    public class OrdenesController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrdenesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        private void AddAuthorizationHeader()
        {
            var token = User.FindFirst("Token")?.Value;
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                AddAuthorizationHeader();
                var response = await _httpClient.GetAsync("ordenes");

                if (response.IsSuccessStatusCode)
                {
                    var ordenes = await response.Content.ReadFromJsonAsync<List<OrdenDto>>();
                    return View(ordenes);
                }

                ModelState.AddModelError("", "Error al cargar las órdenes");
                return View(new List<OrdenDto>());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                return View(new List<OrdenDto>());
            }
        }

        [Authorize(Roles = "Admin,Vendedor")]
        [HttpGet]
        public IActionResult Crear()
        {
            var model = new CrearOrdenViewModel
            {
                Fecha = DateTime.Now,
                Detalles = new List<DetalleOrdenViewModel>()
            };
            return View(model);
        }

        [Authorize(Roles = "Admin,Vendedor")]
        [HttpPost]
        public async Task<IActionResult> Crear(CrearOrdenViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                AddAuthorizationHeader();
                var response = await _httpClient.PostAsJsonAsync("ordenes", model);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Orden creada exitosamente";
                    return RedirectToAction("Index");
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                try
                {
                    var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorContent);
                    foreach (var error in errorResponse.errors)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                catch
                {
                    ModelState.AddModelError("", errorContent);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                AddAuthorizationHeader();
                var response = await _httpClient.GetAsync($"ordenes/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var orden = await response.Content.ReadFromJsonAsync<OrdenDto>();

                    var model = new EditarOrdenViewModel
                    {
                        Id = orden.Id,
                        Fecha = orden.Fecha,
                        Cliente = orden.Cliente,
                        Detalles = orden.Detalles.Select(d => new DetalleOrdenViewModel
                        {
                            Producto = d.Producto,
                            Cantidad = d.Cantidad,
                            PrecioUnitario = d.PrecioUnitario
                        }).ToList()
                    };

                    return View(model);
                }

                TempData["ErrorMessage"] = "Orden no encontrada";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Editar(EditarOrdenViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                AddAuthorizationHeader();
                var response = await _httpClient.PutAsJsonAsync($"ordenes/{model.Id}", model);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Orden actualizada exitosamente";
                    return RedirectToAction("Index");
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                try
                {
                    var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorContent);
                    foreach (var error in errorResponse.errors)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                catch
                {
                    ModelState.AddModelError("", errorContent);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                AddAuthorizationHeader();
                var response = await _httpClient.DeleteAsync($"ordenes/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Orden eliminada exitosamente";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al eliminar la orden";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarDetalle(CrearOrdenViewModel model)
        {
            model.Detalles.Add(new DetalleOrdenViewModel());
            return View("Crear", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoverDetalle(CrearOrdenViewModel model, int index)
        {
            if (model.Detalles.Count > 1 && index >= 0 && index < model.Detalles.Count)
            {
                model.Detalles.RemoveAt(index);
            }
            return View("Crear", model);
        }
    }

    public class OrdenDto
    {
        public Int64 Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }
        public List<DetalleOrdenDto> Detalles { get; set; } = new();
    }

    public class DetalleOrdenDto
    {
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class CrearOrdenViewModel
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "El cliente es requerido")]
        public string Cliente { get; set; }
        [MinLength(1, ErrorMessage = "Debe agregar al menos un detalle")]
        public List<DetalleOrdenViewModel> Detalles { get; set; } = new();
    }

    public class EditarOrdenViewModel
    {
        public Int64 Id { get; set; }
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El cliente es requerido")]
        public string Cliente { get; set; }
        [MinLength(1, ErrorMessage = "Debe agregar al menos un detalle")]
        public List<DetalleOrdenViewModel> Detalles { get; set; } = new();
    }

    public class DetalleOrdenViewModel
    {
        [Required(ErrorMessage = "El producto es requerido")]
        public string Producto { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public decimal Cantidad { get; set; } = 1;
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal => Cantidad * PrecioUnitario;
    }

    public class ErrorResponse
    {
        public List<ErrorDetail> errors { get; set; }
    }

    public class ErrorDetail
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
