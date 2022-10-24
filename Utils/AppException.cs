/*
 * EAD - FuelMe APP API
 *
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
 */

using System.Globalization;

/*
* Common Exception class for Exception handling
*
* @author IT19180526 - S.A.N.L.D. Chandrasiri
* @version 1.0
*/
namespace FuelAppAPI.Utils
{
    public class AppException : Exception
    {
        public AppException() : base()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}