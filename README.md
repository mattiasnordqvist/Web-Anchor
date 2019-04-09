# Web Anchor
[![All Contributors](https://img.shields.io/badge/all_contributors-4-orange.svg?style=flat-square)](#contributors)
[![NuGet version](https://badge.fury.io/nu/webanchor.svg)](http://badge.fury.io/nu/webanchor)  
Web Anchor provides type-safe, testable and flexible, runtime-generated access to web resources.

## Install
To install Web Anchor, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)
<p><code>PM&gt; Install-Package WebAnchor</code></p>

## Use
```csharp
[BaseLocation("api/customer")]
public interface ICustomerApi
{
    [Get("{id}")]
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

Although Web Anchor is off course the best framework for accessing web apis, there are alternatives in the .Net ecosystem you might be interested in. Web Anchor is more or less inspired by [ReFit](https://github.com/paulcbetts/refit/). ReFit uses a completely different approach on how the implementation of your api interfaces is created. While Web Anchor generates an implementation in `runtime` using Castle Windsor DynamicProxy (which means Web Anchor won't work in code distributed through the Windows Phone App Store or whatever it is called), ReFit generates code at `compile time`. There is also [RestSharp](http://restsharp.org/) which I have never tried. It seems to work in a very different way from both ReFit and Web Anchor and it doesn't look extensible at all! (https://github.com/restsharp/RestSharp/issues/932)

## Caveats

When working with HttpClient in any way, you should know some things. Read up on how the HttpClient works and how it is intended to be used, because your intuition is probably wrong.  
http://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/  
http://byterot.blogspot.se/2016/07/singleton-httpclient-dns.html  

## Contributors

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore -->
<table><tr><td align="center"><a href="https://www.carl-berg.se"><img src="https://avatars0.githubusercontent.com/u/209010?v=4" width="100px;" alt="Carl Berg"/><br /><sub><b>Carl Berg</b></sub></a><br /><a href="#ideas-carl-berg" title="Ideas, Planning, & Feedback">🤔</a> <a href="https://github.com/mattiasnordqvist/Web-Anchor/commits?author=carl-berg" title="Code">💻</a> <a href="#review-carl-berg" title="Reviewed Pull Requests">👀</a></td><td align="center"><a href="https://github.com/spinit-moom"><img src="https://avatars2.githubusercontent.com/u/19834760?v=4" width="100px;" alt="Martin Oom"/><br /><sub><b>Martin Oom</b></sub></a><br /><a href="https://github.com/mattiasnordqvist/Web-Anchor/commits?author=spinit-moom" title="Code">💻</a></td><td align="center"><a href="http://www.goatly.net"><img src="https://avatars2.githubusercontent.com/u/4577868?v=4" width="100px;" alt="Mike Goatly"/><br /><sub><b>Mike Goatly</b></sub></a><br /><a href="https://github.com/mattiasnordqvist/Web-Anchor/commits?author=mikegoatly" title="Code">💻</a> <a href="https://github.com/mattiasnordqvist/Web-Anchor/commits?author=mikegoatly" title="Tests">⚠️</a></td><td align="center"><a href="https://github.com/mikaelrjohansson"><img src="https://avatars2.githubusercontent.com/u/17408292?v=4" width="100px;" alt="Mikael Johansson"/><br /><sub><b>Mikael Johansson</b></sub></a><br /><a href="https://github.com/mattiasnordqvist/Web-Anchor/issues?q=author%3Amikaelrjohansson" title="Bug reports">🐛</a></td></tr></table>

<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!
