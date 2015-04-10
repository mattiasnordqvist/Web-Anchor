# WebAnchor

WebAnchor provides clean and type-safe access to web resources.

## Example 1

### Api definition

    [BaseLocation("api/customer")]
    public interface ICustomerApi
    {
        [Get("/{id}")]
        Task<HttpResponseMessage> GetCustomer(int id);
    }

### Api usage
    var customerApi = Api.For<ICustomerApi>("http://localhost:1111/");
    await customerApi.GetCustomer(9);

    // => HTTP GET http://localhost:1111/api/customer/9

## Example 2

### Api definition

    [BaseLocation("api/customer")]
    public interface ICustomerApi
    {
        [Get("")]
        Task<HttpResponseMessage> GetCustomers(string filter = null);
    }

### Api usage
    var customerApi = Api.For<ICustomerApi>("http://localhost:1111/");
    await customerApi.GetCustomers("good");

    // => HTTP GET http://localhost:1111/api/customer?filter=good

## Example 3

### Api definition

	public class Customer
	{
		public int Id { get; set;}
		public string Name { get; set;}
	}

    [BaseLocation("api/customer")]
    public interface ICustomerApi
    {
        [Get("/{id}")]
        Task<Customer> GetCustomer(int id);
    }

### Api usage
    var customerApi = Api.For<ICustomerApi>("http://localhost:1111/");
    var customer = await customerApi.GetCustomer(9);
	var name = customer.Name;

    // => HTTP GET http://localhost:1111/api/customer/9

## Example 4

### Api definition

	public class Customer
	{
		public int Id { get; set;}
		public string Name { get; set;}
	}

    [BaseLocation("api/customer")]
    public interface ICustomerApi
    {
        [Post("")]
        Task<Customer> CreateCustomer(Customer customer);
    }

### Api usage

	var newCustomer = new Customer 
	{
		Id = 1,
		Name = "Mighty Gazelle"
	};

    var customerApi = Api.For<ICustomerApi>("http://localhost:1111/");

    await customerApi.CreateCustomer(newCustomer);

    // => HTTP POST http://localhost:1111/api/customer
	//    {
	//        "Id": 1,
	//        "Name": "Mighty Gazelle"
	//    }
