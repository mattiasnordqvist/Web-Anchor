using System.Collections.Generic;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public interface IApiRequestSettings
    {
        bool InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl { get; set; }
        /// <summary>
        /// Decides if path seperators ("/") in url segment parameters should be considered a part of the url AS PATH SEPERATORS or just as characters.
        /// If you use [BaseLocation({location})] and location is replaced by "api/v2" by some substitution, you probably want the "/" to seperate one path segment "api"
        /// from path segment "v2". If that is how you like it, leave this setting as it is (true). If you want the "/" to be encoded as "%2F" and not look like a path segments 
        /// seperator, set this setting to false.
        /// </summary>
        bool TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators { get; set; }
        bool FormatFormattables { get; set; }
        List<IParameterListTransformer> ParameterListTransformers { get; set; }
        IContentSerializer ContentSerializer { get; set; }
    }

    public interface IApiResponeSettings
    {
        List<IResponseHandler> ResponseHandlers { get; set; }
    }

    public interface IApiSettings
    {
        IApiRequestSettings Request { get; set; }
        IApiResponeSettings Response { get; set; }
    }
}