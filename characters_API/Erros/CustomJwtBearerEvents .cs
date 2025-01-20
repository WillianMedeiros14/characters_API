using Microsoft.AspNetCore.Authentication.JwtBearer;

public class CustomJwtBearerEvents : JwtBearerEvents
{
    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync("{\"error\": \"Token inválido ou ausente\"}");
    }
}
