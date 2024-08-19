using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlockchainManager : MonoBehaviour
{
    public string Address { get; private set; }

    public Button nftButton;
    public Button playButton;
    public Button goldButton;
    public Button meatButton;

    public TextMeshProUGUI nftButtonText;
    public TextMeshProUGUI playButtonText;
    public TextMeshProUGUI goldButtonText;
    public TextMeshProUGUI meatButtonText;

    string NFTAddressSmartContract = "0x996Ad075aeEe889a0e0B3d70ce86EA3e4e5bf66c";
    string GoldAddressSmartContract = "0x9B7C4fA31Cb8Ae00ecb7C441C73915A46C7e3758";

    private void Start()
    {
        nftButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        goldButton.gameObject.SetActive(false);
        meatButton.gameObject.SetActive(false);
    }

    public async void Login()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();        
        Debug.Log(Address);
        Contract contract = ThirdwebManager.Instance.SDK.GetContract(NFTAddressSmartContract);
        List<NFT> nftList = await contract.ERC721.GetOwned(Address);
        if (nftList.Count == 0)
        {
            nftButton.gameObject.SetActive(true);
        }
        else
        {
            playButton.gameObject.SetActive(true);
            goldButton.gameObject.SetActive(true);
            meatButton.gameObject.SetActive(true);
        }
    }

    public async void ClaimNFTPass()
    {
        nftButtonText.text = "Claiming...";
        nftButton.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract(NFTAddressSmartContract);
        var result = await contract.ERC721.ClaimTo(Address, 1);
        nftButtonText.text = "Claimed NFT Pass!";
        nftButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
        goldButton.gameObject.SetActive(true);
        meatButton.gameObject.SetActive(true);
    }

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    public async void ClaimGold()
    {
        goldButtonText.text = "Claiming...";
        goldButton.interactable = false;
        playButton.interactable = false;
        meatButton.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract(GoldAddressSmartContract);
        var result = await contract.ERC20.ClaimTo(Address, "1");

        BlockchainEffect.Instance.gold = 2;

        goldButtonText.text = "x2 Gold";
        goldButton.gameObject.SetActive(false);
        goldButton.interactable = true;
        playButton.interactable = true;
        meatButton.interactable = true;
    }

    public async void ClaimMeat()
    {
        meatButtonText.text = "Claiming...";
        goldButton.interactable = false;
        playButton.interactable = false;
        meatButton.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract(GoldAddressSmartContract);
        var result = await contract.ERC20.ClaimTo(Address, "1");

        BlockchainEffect.Instance.meat = 4;

        goldButtonText.text = "x4 Meat";
        meatButton.gameObject.SetActive(false);
        goldButton.interactable = true;
        playButton.interactable = true;
        meatButton.interactable = true;
    }
}
