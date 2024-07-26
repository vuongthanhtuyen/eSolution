

namespace eShopSolution.ViewModels.Common
{
    public class ApiSeccessResult <T>: ApiResult <T>
    {
        public ApiSeccessResult(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }

        public ApiSeccessResult() {
            IsSuccessed= true;
        }
    }
}
