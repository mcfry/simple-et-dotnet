using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using System;
using System.Threading.Tasks;

public class FirebaseService
{
  public FirebaseService()
  {
    FirebaseApp.Create(new AppOptions
    {
      Credential = GoogleCredential.FromFile("./firebase_auth.json"),
    });
  }

  public async Task<string?> VerifyFirebaseIdTokenAsync(string idToken)
  {
    FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
        .VerifyIdTokenAsync(idToken);
    
    string? uid = decodedToken.Uid;
    if (uid != null) {
      return uid;
    } else {
      return null;
    }
  }
}