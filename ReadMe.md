# WebAnchor

WebAnchor provides clean and type-safe access to web resources.

## Install

Nuget badge here

## Use

    [BaseLocation("api/customer")]
    public interface ICustomerApi
    {
        [Get("/{id}")]
        Task<Customer> GetCustomer(int id);
    }

    ....

    var customerApi = Api.For<ICustomerApi>("http://localhost:1111/");
    await customerApi.GetCustomer(9);

    // => HTTP GET http://localhost:1111/api/customer/9

