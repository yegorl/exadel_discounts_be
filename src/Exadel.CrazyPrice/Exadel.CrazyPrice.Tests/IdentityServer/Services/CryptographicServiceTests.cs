using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.IdentityServer;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Services;
using Xunit;

namespace Exadel.CrazyPrice.Tests.IdentityServer.Services
{
    public class CryptographicServiceTests
    {
        private readonly ICryptographicService _cryptographicService;
        public CryptographicServiceTests()
        {
            _cryptographicService = new CryptographicService();
        }

        [Theory]
        [InlineData(
            "1111",
            "+SzcAYCGZcD+eov22e4GpybKoLsaoguAIPS0D3Ntj2ATCpcQvvbsitFLFT1IZOTEzjvxTOQ/ISdxSW9x+VmtV1UFTGgIgKPZPKMtkWmyL7SOza6iuFbikXRr6olGNkdRK6KEOSyY0d+S1VZ9RbcGl9eYfppPM5s8xVc7w0FsODw=",
            "IZmh6o3UgDIHs5rXD3L5hD2momUnLQdyJUrpQNW/"
            )]
        [InlineData(
            "2222",
            "+fs8G3LGPEg3HFHyJNfv4jAe+a9zKCj4AgZDBVatsGcHW625Ce+QO6cTcI/oLFn2ArUvvYPs7Hs664OPojKm3A6kPAQ77ysqzigxg75xbKS2Cel5Un7BaIBMN+BFRU5CnaXnPQ4rmOENf8p70FbdwWr359pvmTttFWsQAG2iCjI=",
            "sOVqvk4NWiJ4gbTeVJ19M3V2gG5jOdootBQbTv6v"
        )]
        public void ComparePasswordHash_True(string password, string hashedPassword, string salt)
        {
            //Arrange

            //Act    
            var result = _cryptographicService.ComparePasswordHash(password, hashedPassword, salt);
            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(
            "124",
            "+SzcAYCGZcD+eov22e4GpybKoLsaoguAIPS0D3Ntj2ATCpcQvvbsitFLFT1IZOTEzjvxTOQ/ISdxSW9x+VmtV1UFTGgIgKPZPKMtkWmyL7SOza6iuFbikXRr6olGNkdRK6KEOSyY0d+S1VZ9RbcGl9eYfppPM5s8xVc7w0FsODw=",
            "IZmh6o3UgDIHs5rXD3L5hD2momUnLQdyJUrpQNW/"
        )]
        [InlineData(
            "2222",
            "+fs8G3LGPEg3ZEHyJNfv4jAe+a9zKCj4AgZDBVatsGcHW625Ce+QO6cTcI/oLFn2ArUvvYPs7Hs664OPojKm3A6kPAQ77ysqzigxg75xbKS2Cel5Un7BaIBMN+BFRU5CnaXnPQ4rmOENf8p70FbdwWr359pvmTttFWsQAG2iCjI=",
            "hfynat4NWiJ4gbTeVJ19M3V2gG5jOdootBQbTv6v"
        )]
        public void ComparePasswordHash_False(string password, string hashedPassword, string salt)
        {
            //Arrange

            //Act    
            var result = _cryptographicService.ComparePasswordHash(password, hashedPassword, salt);
            //Assert
            Assert.False(result);
        }
    }
}