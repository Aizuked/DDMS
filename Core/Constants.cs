namespace Core;

public class Constants
{
    public static char   ENV_DIR_SEP   => Path.DirectorySeparatorChar;
    public static string ENV_FILE_PATH => Environment.CurrentDirectory + ENV_DIR_SEP + "Files" + ENV_DIR_SEP;

    public static string IMAGES_THUMB_POSTFIX => "_thumb";
}