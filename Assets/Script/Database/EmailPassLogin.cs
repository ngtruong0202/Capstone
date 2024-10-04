using System.Collections;
using UnityEngine;
using TMPro;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase;
using UnityEngine.UI;
using Unity.VisualScripting;

public class EmailPassLogin : MonoBehaviour
{
    FirebaseAuth auth;
    [SerializeField] DataSever dataSever;
    PlayerInfo playerInfo;

    #region variables
    [Header("Login")]
    public Button loginButton;
    public InputField LoginEmail;
    public InputField loginPassword;

    [Header("Sign up")]
    public Button signButton;
    public InputField SignupEmail;
    public InputField SignupPassword;
    public InputField SignupPasswordConfirm;

    [Header("Forger Pass")]
    public Button forgetButton;
    public InputField ForgetEmail;


    [Header("Extra")]
    public GameObject loadingScreen;
    public TextMeshProUGUI logTxt;
    public GameObject loginUI, signupUI, createCharactorUI;

    [Header("Extra")]
    public InputField playerName;

    public Button quitGame;
    public Button createCharactorButton;

    int playercharactorLimit = 20;
    int playerMincharactorLimit = 4;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        loginUI.SetActive(true);
        signupUI.SetActive(false);
        createCharactorUI.SetActive(false);

        loginButton.onClick.AddListener(Login);
        signButton.onClick.AddListener(SignUp);
        forgetButton.onClick.AddListener(FogetPassword);
        quitGame.onClick.AddListener(QuitGame);
        createCharactorButton.onClick.AddListener(CreateCharactor);

        playerName.characterLimit = playercharactorLimit;
    }

    #endregion

    #region signup 
    public void SignUp()
    {
        loadingScreen.SetActive(true);

        string email = SignupEmail.text;
        string password = SignupPassword.text;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            // Firebase user has been created.

            loadingScreen.SetActive(false);
            AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            SignupEmail.text = "";
            SignupPassword.text = "";
            SignupPasswordConfirm.text = "";

            if (result.User.IsEmailVerified)
            {
                //showLogMsg("Sign up Successful");
                Debug.Log("Email đã được sử dụng");
            }
            else
            {
                //showLogMsg("Please verify your email!!");
                SendEmailVerification();
            }

        });
    }

    public void SendEmailVerification()
    {
        StartCoroutine(SendEmailForVerificationAsync());
    }

    IEnumerator SendEmailForVerificationAsync()
    {
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            var sendEmailTask = user.SendEmailVerificationAsync();
            yield return new WaitUntil(() => sendEmailTask.IsCompleted);

            if (sendEmailTask.Exception != null)
            {
                print("Email send error");
                FirebaseException firebaseException = sendEmailTask.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError)firebaseException.ErrorCode;

                switch (error)
                {
                    case AuthError.None:
                        break;
                    case AuthError.Unimplemented:
                        break;
                    case AuthError.Failure:
                        break;
                    case AuthError.InvalidCustomToken:
                        break;
                    case AuthError.CustomTokenMismatch:
                        break;
                    case AuthError.InvalidCredential:
                        break;
                    case AuthError.UserDisabled:
                        break;
                    case AuthError.AccountExistsWithDifferentCredentials:
                        break;
                    case AuthError.OperationNotAllowed:
                        break;
                    case AuthError.EmailAlreadyInUse:
                        break;
                    case AuthError.RequiresRecentLogin:
                        break;
                    case AuthError.CredentialAlreadyInUse:
                        break;
                    case AuthError.InvalidEmail:
                        break;
                    case AuthError.WrongPassword:
                        break;
                    case AuthError.TooManyRequests:
                        break;
                    case AuthError.UserNotFound:
                        break;
                    case AuthError.ProviderAlreadyLinked:
                        break;
                    case AuthError.NoSuchProvider:
                        break;
                    case AuthError.InvalidUserToken:
                        break;
                    case AuthError.UserTokenExpired:
                        break;
                    case AuthError.NetworkRequestFailed:
                        break;
                    case AuthError.InvalidApiKey:
                        break;
                    case AuthError.AppNotAuthorized:
                        break;
                    case AuthError.UserMismatch:
                        break;
                    case AuthError.WeakPassword:
                        break;
                    case AuthError.NoSignedInUser:
                        break;
                    case AuthError.ApiNotAvailable:
                        break;
                    case AuthError.ExpiredActionCode:
                        break;
                    case AuthError.InvalidActionCode:
                        break;
                    case AuthError.InvalidMessagePayload:
                        break;
                    case AuthError.InvalidPhoneNumber:
                        break;
                    case AuthError.MissingPhoneNumber:
                        break;
                    case AuthError.InvalidRecipientEmail:
                        break;
                    case AuthError.InvalidSender:
                        break;
                    case AuthError.InvalidVerificationCode:
                        break;
                    case AuthError.InvalidVerificationId:
                        break;
                    case AuthError.MissingVerificationCode:
                        break;
                    case AuthError.MissingVerificationId:
                        break;
                    case AuthError.MissingEmail:
                        break;
                    case AuthError.MissingPassword:
                        break;
                    case AuthError.QuotaExceeded:
                        break;
                    case AuthError.RetryPhoneAuth:
                        break;
                    case AuthError.SessionExpired:
                        break;
                    case AuthError.AppNotVerified:
                        break;
                    case AuthError.AppVerificationFailed:
                        break;
                    case AuthError.CaptchaCheckFailed:
                        break;
                    case AuthError.InvalidAppCredential:
                        break;
                    case AuthError.MissingAppCredential:
                        break;
                    case AuthError.InvalidClientId:
                        break;
                    case AuthError.InvalidContinueUri:
                        break;
                    case AuthError.MissingContinueUri:
                        break;
                    case AuthError.KeychainError:
                        break;
                    case AuthError.MissingAppToken:
                        break;
                    case AuthError.MissingIosBundleId:
                        break;
                    case AuthError.NotificationNotForwarded:
                        break;
                    case AuthError.UnauthorizedDomain:
                        break;
                    case AuthError.WebContextAlreadyPresented:
                        break;
                    case AuthError.WebContextCancelled:
                        break;
                    case AuthError.DynamicLinkNotActivated:
                        break;
                    case AuthError.Cancelled:
                        break;
                    case AuthError.InvalidProviderId:
                        break;
                    case AuthError.WebInternalError:
                        break;
                    case AuthError.WebStorateUnsupported:
                        break;
                    case AuthError.TenantIdMismatch:
                        break;
                    case AuthError.UnsupportedTenantOperation:
                        break;
                    case AuthError.InvalidLinkDomain:
                        break;
                    case AuthError.RejectedCredential:
                        break;
                    case AuthError.PhoneNumberNotFound:
                        break;
                    case AuthError.InvalidTenantId:
                        break;
                    case AuthError.MissingClientIdentifier:
                        break;
                    case AuthError.MissingMultiFactorSession:
                        break;
                    case AuthError.MissingMultiFactorInfo:
                        break;
                    case AuthError.InvalidMultiFactorSession:
                        break;
                    case AuthError.MultiFactorInfoNotFound:
                        break;
                    case AuthError.AdminRestrictedOperation:
                        break;
                    case AuthError.UnverifiedEmail:
                        break;
                    case AuthError.SecondFactorAlreadyEnrolled:
                        break;
                    case AuthError.MaximumSecondFactorCountExceeded:
                        break;
                    case AuthError.UnsupportedFirstFactor:
                        break;
                    case AuthError.EmailChangeNeedsVerification:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                print("Email successfully send");
            }
        }
    }


    #endregion

    #region Login
    public void Login()
    {
        loadingScreen.SetActive(true);

        string email = LoginEmail.text;
        string password = loginPassword.text;

        Credential credential =
        EmailAuthProvider.GetCredential(email, password);
        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }
            loadingScreen.SetActive(false);
            AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            if (result.User.IsEmailVerified)
            {
                ReadData();
                //showLogMsg("Log in Successful");

            }
            else
            {
                showLogMsg("Please verify email!!");

            }

        });
    }

    public void FogetPassword()
    {
        string email = ForgetEmail.text;
        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted) return;
            if (task.IsCanceled) return;

        });
    }

    public void LogOut()
    {
        auth.SignOut();
        return;
    }

    #region
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

    public void ReadData()
    {

        dataSever.LoadDataFn<PlayerInfo>("User/" + auth.CurrentUser.UserId, (loaded) =>
        {
            if (loaded != null)
            {
                if (loaded.playerName != null)
                {
                    GameManager.instance.LoadScene(2, 3);

                }

            }
            else
            {
                loginUI.SetActive(false);
                createCharactorUI.SetActive(true);
                Debug.Log("No data found or failed to load data.");
            }
        });
    }

    public void CreateCharactor()
    {
        string playername = playerName.text;
        PlayerInfo playerInfo = new PlayerInfo(auth.CurrentUser.UserId, playername);
        Debug.Log(playerInfo);
        
        if(IsPlayerNameInputValid())
        {
            dataSever.SaveDataFn("User/" + auth.CurrentUser.UserId, playerInfo);
            GameManager.instance.LoadScene(2, 3);
        }

    }

    private bool IsPlayerNameInputValid()
    {
        string names = playerName.text;
        if (string.IsNullOrEmpty(names))
        {
            Debug.Log("name không được để trống");
            return false;
        }
        if (names.Length < playerMincharactorLimit)
        {
            Debug.Log("name phải hơn 4 ký tự");

            return false;
        }

        return true;
    }
        #endregion

    #region extra
        void showLogMsg(string msg)
    {
        logTxt.text = msg;
        logTxt.GetComponent<Animation>().Play("textFadeout");
    }
    #endregion

}