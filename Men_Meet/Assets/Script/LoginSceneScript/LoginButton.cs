using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginButton : MonoBehaviour
{
    [SerializeField] private InputField UIDInputText;
    [SerializeField] private InputField UPWInputText;
    [SerializeField] string Signup;
    [SerializeField] string Introduce;
    [SerializeField] private GameObject FailedPanel;
    [SerializeField] private UserStateScript stateScript;
    //테스트용 코드(삭제 예정)
    void Start()
    {
        UIDInputText.text = "manager1";
        UPWInputText.text = "mentoss123456";
    }
    //회원가입 버튼 클릭 시 
   public void SignupClick() => Application.OpenURL(Signup);
    //로그인 버튼 클릭 시
    public void LoginClick() => StartCoroutine(Login());
    //Men-Meet소개 텍스트 클릭
    public void IntroduceClick() => Application.OpenURL(Introduce);
    //로그인 실패 안내 통지메세지
    public void ShowFailedPanel() => FailedPanel.SetActive(true);

    public void CloseFailedPanel() => FailedPanel.SetActive(false);
    //로그인 코루틴
    public IEnumerator Login()
    {
        string serverid = "UID="+UIDInputText.text;
        string serverpw = "UPW="+UPWInputText.text;
        string serverPath = "http://mentoss123.cafe24.com/SungjinTest/MetaLogin.jsp?"+serverid+"&"+serverpw;
        Debug.Log(serverPath);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(serverPath)) 
        {
            yield return webRequest.SendWebRequest(); 
                
            if (webRequest.isNetworkError || webRequest.isHttpError){
                Debug.Log(webRequest.error);
            }
            else
            {
                string result = webRequest.downloadHandler.text;
                string[] resultCmd = result.Split('$');
                Debug.Log(result);

                if (resultCmd[0].Trim().Equals("Correct"))
                {
                    Debug.Log("로그인 성공했습니다.");
                    stateScript.UserNickName = resultCmd[1].Trim();
                    LoginSucceed();
                }
                else
                {
                    Debug.Log("로그인 실패했습니다.");
                    ShowFailedPanel();
                }

                UIDInputText.text = "";
                UPWInputText.text = "";
            }
        }
    }
    //로그인 성공 시 다음 씬
    void LoginSucceed()
    {
        stateScript.UserId = UIDInputText.text;
        SceneManager.LoadScene(1);
    }
}