# WebAnchor

WebAnchor provides clean and type-safe access to web resources.

## Install

To install Web anchor, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)
        <p><code>PM&gt; Install-Package WebAnchor</code></p>


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

