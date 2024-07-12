namespace Foundation.Extension.Domain.Models
{
    public class Error
    {
        public int HttpStatus { get; set; }
        public string Code { get; set; }
        public string ContentType { get; set; } = "text/plain";
    }

    public static class ErrorCode
    {
        public const string MissingOrganisation       = "errors.missingorganisation";       // 400
        public const string MissingLanguage           = "errors.missinglanguage";           // 400
        public const string MissingUserApplication    = "errors.missinguserapplication";           // 400
        public const string InvalidChart              = "errors.invalidchart";              // 400
        public const string AuthenticationFailed      = "errors.authenticationfailed";      // 401
        public const string NoRegisteredUser          = "errors.noregistereduser";          // 401
        public const string UnauthorizedOrganisation  = "errors.unauthorizedorganisation";  // 403
        public const string UnauthorizedExtension     = "errors.unauthorizedextension";     // 403
        public const string UnauthorizedUser          = "errors.unauthorizeduser";          // 403
        public const string NoAdminPrivilege          = "errors.noadminprivilege";          // 403
        public const string BadCredentials            = "errors.badcredentials";            // 403
        public const string UserNotInOrganisation     = "errors.usernotinorganisation";     // 403
        public const string EntityNotFound            = "errors.entitynotfound";            // 404
        public const string ImageNotFound             = "errors.imagenotfound";             // 404
        public const string TokenNotFound             = "errors.tokennotfound";             // 404
        public const string AlreadyRegisteredUser     = "errors.alreadyregistereduser";     // 409
        public const string DeviceAlreadyInOrganisation = "errors.devicealreadyinorganisation"; // 409
        public const string UserAlreadyInOrganisation = "errors.useralreadyinorganisation"; // 409
        public const string EmailFailure              = "errors.emailfailure";              // 422
        public const string RequireValidation         = "errors.requirevalidation";         // 422
        public const string DatabaseError             = "errors.databaseerror";             // 449
        public const string GraphSettingsError        = "errors.graphsettingserror";        // 500
        public const string GraphUserError            = "errors.graphusererror";            // 500
        public const string NoMappableProperty        = "errors.nomappableproperty";        // 500
        public const string InvalidExtensionCookie    = "errors.unvalidextensioncookie";    // 500
        public const string UnexpectedError           = "errors.unexpectederror";           // 500
    }
}