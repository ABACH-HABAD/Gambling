namespace Gambling.WebApi.Classes;

internal class AdminRole
{
    internal static string Name = "Админ";

    /// <summary>
    /// 3 - значение по умолчанию для роли администратора
    /// При вызове Init() это значение должно обновится до актуально 
    /// </summary>
    private readonly static int _adminRoleId = 3;

    internal static string Id = _adminRoleId.ToString();
}