namespace Trivadis.AzureBootcamp.WebApi.Models
{
    public class ChatUser
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string UserId { get; set; }
    }


    static class Avatar
    {
        private static int _svg = 0;

        public static string GetNextAvatarSvg()
        {
            ++_svg;
            if (_svg > 16)
            {
                _svg = 1;
            }

            return string.Format("svg-{0}", _svg);
        }
    }
}