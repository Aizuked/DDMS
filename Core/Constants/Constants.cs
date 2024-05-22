namespace Core.Constants;

public class Constants
{
    public static char   ENV_DIR_SEP           => Path.DirectorySeparatorChar;
    public static string ENV_FILE_PATH         => Environment.CurrentDirectory + ENV_DIR_SEP + "Files" + ENV_DIR_SEP;

    public static string IMAGES_THUMB_POSTFIX  => "_thumb";

    public static string ROLES_ADMIN           => "ADMIN";
    public static string ROLES_TEACHER         => "TEACHER";
    public static string ROLES_STUDENT         => "STUDENT";

    public static string NOTIFY_SUCCESS        => "Успешно!";
}