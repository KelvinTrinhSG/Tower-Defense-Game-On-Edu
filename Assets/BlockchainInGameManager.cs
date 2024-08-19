using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;
using TMPro;

public class BlockchainInGameManager : MonoBehaviour
{
    public string Address { get; private set; }

    public Button text_CostButton;
    public Button button_Continue;
    public TextMeshProUGUI button_ContinueText;
    public Button button_ClaimAD;
    public TextMeshProUGUI button_ClaimADText;
    public Button reward_Gold;
    public TextMeshProUGUI reward_GoldText;
    public Button claimX2;
    public TextMeshProUGUI claimX2Text;

    string GoldAddressSmartContract = "0x9B7C4fA31Cb8Ae00ecb7C441C73915A46C7e3758";

    public async void ClaimGem()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        text_CostButton.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract(GoldAddressSmartContract);
        var result = await contract.ERC20.ClaimTo(Address, "1");

        GameManager.instance.uiManager.storeView.GetFreeGemsCB();

        text_CostButton.interactable = true;
    }

    public async void ContinueGame()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        button_Continue.interactable = false;
        button_ContinueText.text = "Processing!";
        var contract = ThirdwebManager.Instance.SDK.GetContract(GoldAddressSmartContract);
        var result = await contract.ERC20.ClaimTo(Address, "1");

        GameManager.instance.uiManager.retriveView.RetriveByRewardVideoCB();

        button_Continue.interactable = true;
        button_ContinueText.text = "Continue";
    }

    public async void RewardClaim()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        button_ClaimAD.interactable = false;
        button_ClaimADText.text = "Processing!";
        var contract = ThirdwebManager.Instance.SDK.GetContract(GoldAddressSmartContract);
        var result = await contract.ERC20.ClaimTo(Address, "1");

        GameManager.instance.uiManager.resultView.ClaimX2CB();

        button_ClaimAD.interactable = true;
        button_ClaimADText.text = "x2 Claim";
    }

    public async void Reward4xGold()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        reward_Gold.interactable = false;
        reward_GoldText.text = "Processing!";
        var contract = ThirdwebManager.Instance.SDK.GetContract(GoldAddressSmartContract);
        var result = await contract.ERC20.ClaimTo(Address, "1");

        GameManager.instance.uiManager.gameView.GetX4GoldCB();

        reward_Gold.interactable = true;
        reward_GoldText.text = "X4";
    }

    public async void Reward2xChest()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        claimX2.interactable = false;
        claimX2Text.text = "Processing!";
        var contract = ThirdwebManager.Instance.SDK.GetContract(GoldAddressSmartContract);
        var result = await contract.ERC20.ClaimTo(Address, "1");

        GameManager.instance.uiManager.rewardView.ClaimX2CB();

        claimX2.interactable = true;
        claimX2Text.text = "X2 CLAIM";
    }
}
