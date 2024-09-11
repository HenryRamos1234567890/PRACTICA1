var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Crear un alista para almacenar objetos de tipo Product (Productos)
var products = new List<Product>();

//Configurar una ruta GET para obtener todos los productos
app.MapGet("/products", () =>
{
    return products; //Devuelve la lista de productos
});

//Configurar una ruta GET pa' obtener un producto especifico por su ID
app.MapGet("/products/{id}", (int id) =>
{
    //Busca un producto en la lista que tenga el ID especificado
    var product = products.FirstOrDefault(x => x.Id == id);
    return product; //Devuelve el producto encontrado (o null si no se encuentra)
});

//Configurar una ruta POST para agregar un nuevo cliente a la lista
app.MapPost("/products", (Product product) =>
{
    products.Add(product); //Agregar el nuevo producto a la lista
    return Results.Ok(); //Devuelve una respuesta HTTP 200 OK
});

//Configurar una ruta PUT para actualizar un producto existente por su ID
app.MapPut("/products/{id}", (int id, Product product) =>
{
    //Buscar un producto en la lista que tenga el ID especificado
    var existingProduct = products.FirstOrDefault(x => x.Id == id);
    if (existingProduct != null)
    {
        //Actualizar los datos del producto existente con los datos proporcionados
        existingProduct.Name = product.Name;
        existingProduct.Precio = product.Precio;
        return Results.Ok(); //Devuelve una respuesta HTTP 200 OK
    }
    else
    {
        return Results.NotFound(); //Devuelve una respuesta HTTP 404 Not Found si el producto no existe
    }
});

//Configurar una ruta DELETE para eliminar un producto por su ID
app.MapDelete("/products/{id}", (int id) =>
{
    //Buscar un producto en la lista que tenga el ID especificado
    var existingProduct = products.FirstOrDefault(x => x.Id == id);
    if (existingProduct != null)
    {
        //Elimina el producto de la lista
        products.Remove(existingProduct);
        return Results.Ok(); //Devuelve una respuesta HTTP 200 OK
    }
    else
    {
        return Results.NotFound(); //Devuelve una respuesta HTTP 404 Not Found si el producto no existe
    }
});

app.UseAuthorization();

app.MapControllers();

//Ejecutar la aplicacion
app.Run();

//Definicion de la clase Product que representa la estructura de un producto
internal class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Precio { get; set; }
}
