# Web Anchor
[![Build status](https://ci.appveyor.com/api/projects/status/98vo2qacd6o53wer?svg=true)](https://ci.appveyor.com/project/mattiasnordqvist/web-anchor)
[![NuGet version](https://badge.fury.io/nu/webanchor.svg)](http://badge.fury.io/nu/webanchor)  
Web Anchor provides type-safe, testable and crisp access to web resources.

## Install
NB! There is currently a problem with the Json.NET dependency. After installation of Web Anchor, you need to update Json.NET to v7.0.1. See https://github.com/mattiasnordqvist/Web-Anchor/issues/58  
To install Web Anchor, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)
<p><code>PM&gt; Install-Package WebAnchor</code></p>

## Use
```csharp
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
```

Does this get you started? Head over to the [wiki](https://github.com/mattiasnordqvist/Web-Anchor/wiki) for more coolness.

## Collaborate

Web Anchor is open-sourced to the max and free to use and modify, even for commercial projects. We would love to hear from you if you're using Web Anchor.

Also, if you like (or hate) Web Anchor so much that you would like to contribute in any way, please visit us on [GitHub](https://github.com/mattiasnordqvist/Web-Anchor). :) 

## Alternatives

Although Web Anchor is off course the best framework for accessing web apis, there are alternatives in the .Net ecosystem you might be interested in. Web Anchor is more or less inspired by [ReFit](https://github.com/paulcbetts/refit/). ReFit uses a completely different approach on how the implementation of your api interfaces is created. While Web Anchor generates an implementation in `runtime` using Castle Windsor DynamicProxy, ReFit generates code at `compile time`. ReFit says it supports platforms like Xamarin. Web Anchor has only been tested on windows desktops and servers. If you´ve used Web Anchor successfully on any other platform, let us know! :) There is also [RestSharp](http://restsharp.org/) which I have never tried. It seems to work in a very different way from both ReFit and Web Anchor
