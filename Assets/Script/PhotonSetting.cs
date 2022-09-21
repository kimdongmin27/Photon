using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


public class PhotonSetting : MonoBehaviour
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] InputField username;
    [SerializeField] Dropdown region;


    private void Awake()
    {
        PlayFabSettings.TitleId = "97ABB";
    }

    // LoginResult <- �α��� ���� ���θ� ��ȯ�մϴ�
    public void LoginSuccess(LoginResult result)
    {
        // AutomaticallySyncScene ������ Ŭ���̾�Ʈ�� �������� ���� ����ȭ���� ������  �����ϴ� ����Դϴ�.
        // false = ����ȭ�� ���� �ʰڴ�.
        // true = ������ Ŭ���̾�Ʈ�� �������� ����ȭ�� �ϰڴ�.
        PhotonNetwork.AutomaticallySyncScene = false;

        // ���� ������ �������� ������ �����մϴ�.
        // ���� ������ ������ �� �ֵ��� ���ڿ� ����� �����մϴ�.
        PhotonNetwork.GameVersion = "1.0f";

        // ���� ���̵� ����
        PhotonNetwork.NickName = username.text;

        // �Է��� ������ �����մϴ�.
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.options[region.value].text;

        // ���� ����
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void LoginFailure(PlayFabError error)
    {
        Debug.Log("�α��� ����");
    }

    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("ȸ�� ���� ����");
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("ȸ�� ���� ����");
    }

    public void SignUp()
    {
        //RegisterPlayFabUserRequest : ������ ������ ����ϱ� ���� Ŭ����
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,       // �Է��� email
            Password = password.text, // �Է��� ��й�ȣ
            Username = username.text, // �Է��� ���� �̸�
        };

        PlayFabClientAPI.RegisterPlayFabUser
        (
               request,       // ȸ�����Կ� ���� ���� ����
               SignUpSuccess, // ȸ�������� �������� �� ȸ������ ���� �Լ� ȣ��
               SignUpFailure  // ȸ�������� �������� �� ȸ������ ���� �Լ� ȣ��
        );
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text
        };

        PlayFabClientAPI.LoginWithEmailAddress
        (
            request,
            LoginSuccess,
            LoginFailure
        );

    }
}
