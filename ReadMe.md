# Web Anchor

Web Anchor provides clean and type-safe access to web resources.

## Install

To install Web Anchor, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)
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

## Collaborate
Web Anchor is open-sourced to the max and free to use and modify, even for commercial projects. We would love to hear from you if you're using Web Anchor.

Also, if you like Web Anchor so much that you would like to contribute in any way, please visit us on [GitHub](https://github.com/mattiasnordqvist/Web-Anchor). :) 

## Alternatives

Although Web Anchor is by far the best module for accessing web apis, there are alternatives in the .Net ecosystem you might be interested in. Web Anchor is more or less inspired by [ReFit](https://github.com/paulcbetts/refit/). ReFit uses a completely different approach on how the implementation of your api interfaces is created. While Web Anchor generates an implementation in `runtime` using Castle Windsor DynamicProxy, ReFit generates code at `compile time`. ReFit says it supports platforms like Xamarin and Windows Phone. Web Anchor as only been tested on windows desktops and servers.
