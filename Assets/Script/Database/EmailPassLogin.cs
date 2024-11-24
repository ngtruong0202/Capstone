using System.Collections;
using UnityEngine;
using TMPro;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase;
using UnityEngine.UI;
using System.Threading.Tasks;


public class EmailPassLogin : MonoBehaviour
{
    FirebaseAuth auth;
    PlayerInfo playerInfo;

    #region variables
    [Header("Login")]
    public Button loginButton;
    public InputField LoginEmail;
    public InputField loginPassword;
    public TextMeshProUGUI loginText;

    [Header("Sign up")]
    public Button signButton;
    public InputField SignupEmail;
    public InputField SignupPassword;
    public InputField SignupPasswordConfirm;
    public TextMeshProUGUI SignText;

    [Header("Reset Pass")]
    public Button resetPassButton;
    public InputField resetPassEmail;
    public TextMeshProUGUI resetPassText;

    //[Header("Change Pass")]
    //public InputField newPasswordInputField;
    //public InputField newPasswordInputField2;

    [Header("Extra")]
    public GameObject loadingScreen;
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
        signButton.onClick.AddListener(Register);
        resetPassButton.onClick.AddListener(ResetPass);
        quitGame.onClick.AddListener(QuitGame);
        createCharactorButton.onClick.AddListener(CreateCharactor);

        playerName.characterLimit = playercharactorLimit;
    }

    #endregion

    public async void Register()
    {
        loadingScreen.SetActive(true);

        string email = SignupEmail.text;
        string password = SignupPassword.text;
        string password2 = SignupPasswordConfirm.text;
        if(password == password2)
        {
            await RegisterUser(email, password);
        }
        else
        {
            SignText.text = "Mật khẩu không trùng khớp";
            loadingScreen.SetActive(false);
        }
    }

    public async Task RegisterUser(string email, string password)
    {
        try
        {
            // Tạo người dùng mới
            AuthResult authResult = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            FirebaseUser newUser = authResult.User;

            SignupEmail.text = "";
            SignupPassword.text = "";
            SignupPasswordConfirm.text = "";

            // Gửi email xác thực
            await SendVerificationEmail(newUser);
            loadingScreen.SetActive(false);
        }
        catch (FirebaseException e)
        {
            Debug.Log("Lỗi đăng ký: " + e.Message);
            if (e.ErrorCode == (int)AuthError.EmailAlreadyInUse)
            {
                SignText.text = "Email đã được sử dụng.";
            }
            else if (e.ErrorCode == (int)AuthError.InvalidEmail)
            {
                SignText.text = "Email không hợp lệ.";
            }
            else if (e.ErrorCode == (int)AuthError.InvalidEmail)
            {
                SignText.text = "Email không hợp lệ.";
            }
            else if (e.ErrorCode == (int)AuthError.WeakPassword)
            {
                SignText.text = "Mật khẩu không đủ mạnh.";
            }
            // Xử lý lỗi đăng ký (ví dụ: hiển thị thông báo cho người dùng)
            loadingScreen.SetActive(false);
        }
    }

    private async Task SendVerificationEmail(FirebaseUser user)
    {
        try
        {
            await user.SendEmailVerificationAsync();
            Debug.Log("Email xác thực đã được gửi tới: " + user.Email);
            SignText.text = "Email xác thực đã được gửi tới: " + user.Email;
            // Thông báo cho người dùng kiểm tra email của họ
            loadingScreen.SetActive(false);
        }
        catch (System.Exception e)
        {
            Debug.Log("Lỗi gửi email xác thực: " + e.Message);

            // Xử lý lỗi gửi email (ví dụ: hiển thị thông báo cho người dùng)
            loadingScreen.SetActive(false);
        }
    }

    public async void Login()
    {
        loadingScreen.SetActive(true);

        string email = LoginEmail.text;
        string password = loginPassword.text;
        await LoginUser(email, password);
    }

    public async Task LoginUser(string email, string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            loginText.text = "Vui lòng nhập mật khẩu.";
            loadingScreen.SetActive(false);
            return;
        }

        try
        {
            var authResult = await auth.SignInWithEmailAndPasswordAsync(email, password);
            if (!authResult.User.IsEmailVerified)
            {
                loginText.text = "Email chưa được xác thực. Vui lòng kiểm tra email của bạn.";

                // Gửi email xác thực
                await authResult.User.SendEmailVerificationAsync();
                loginText.text += "\nEmail xác thực đã được gửi lại.";
            }
            else
            {
                // Email đã được xác thực
                ReadData();
            }
            loadingScreen.SetActive(false);

        }

        catch (FirebaseException e)
        {
            if (e.ErrorCode == (int)AuthError.InvalidEmail)
            {
                loginText.text = "Email không hợp lệ.";
            }
            else if (e.ErrorCode == (int)AuthError.WrongPassword)
            {
                loginText.text = "Mật khẩu không chính xác.";
            }
            else if(e.ErrorCode == 1)
            {
                loginText.text = "Sai tài khoản hoặc mật khẩu.";
            }
            else
            {
                Debug.Log("Lỗi đăng nhập: " + e.Message);
                loginText.text = e.Message;
            }

            loadingScreen.SetActive(false);
        }
    }

    public async void ResetPass()
    {
        loadingScreen.SetActive(true);

        string email = resetPassEmail.text;
        await SendPasswordResetEmail(email);
    }
    public async Task SendPasswordResetEmail(string email)
    {
        try
        {
            await auth.SendPasswordResetEmailAsync(email);
            resetPassText.text = "Email khôi phục mật khẩu đã được gửi thành công.";
            loadingScreen.SetActive(false);
        }
        catch (FirebaseException e)
        {
            if (e.ErrorCode == (int)AuthError.UserNotFound)
            {
                resetPassText.text = "Không tìm thấy tài khoản với email này.";
            }
            else
            {
                Debug.Log("Lỗi khi gửi email khôi phục mật khẩu: " + e.Message);
                resetPassText.text = "Lỗi khi gửi email khôi phục mật khẩu.";
            }
            loadingScreen.SetActive(false);
        }

    }

    public async Task ChangePassword(string newPassword)
    {
        FirebaseUser user = auth.CurrentUser;

        if (user != null)
        {
            try
            {
                await user.UpdatePasswordAsync(newPassword);
                Debug.Log("Đổi mật khẩu thành công.");
            }
            catch (FirebaseException e)
            {
                Debug.Log("Lỗi khi đổi mật khẩu: " + e.Message);
            }
        }
        else
        {
            Debug.Log("Không có người dùng nào đang đăng nhập.");
        }
    }

    public void LogOut()
    {
        auth.SignOut();
        return;
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ReadData()
    {
        DataSever.Instance.LoadDataFn<PlayerInfo>("User/" + auth.CurrentUser.UserId, (loaded) =>
        {
            if (loaded != null)
            {
                if (loaded.playerName != null)
                {
                    //truyền vào scene đang ở
                    GameManager.instance.StartGame("Map");
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
        
        if(IsPlayerNameInputValid())
        {
            DataSever.Instance.SaveDataFn("User/" + auth.CurrentUser.UserId, playerInfo);
            GameManager.instance.StartGame("Map");
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
}