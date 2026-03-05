using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
public class FirebaseService
{
private readonly FirebaseClient _firebase;
public FirebaseService()
{
_firebase = new FirebaseClient("https://airsensorai-default-rtdb.firebaseio.com/");
}
public async Task SalvarDadosAsync(DeviceDto dados)
{
await _firebase
.Child("qualidade_ar")
.PostAsync(dados);
}
}
