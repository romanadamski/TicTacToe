using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : BaseManager<UIManager>
{
	[Inject]
	DiContainer _diContainer;

	[SerializeField]
    private List<BaseMenu> menuPrefabs;
    [SerializeField]
    private Image backgroundPrefab;

	public Sprite PlayerOne;
	public Sprite PlayerTwo;

	private List<BaseMenu> _menus = new List<BaseMenu>();
	private Image backgroundImage;

	private void Awake()
    {
        CreateMenus();
        HideAllMenus();
        CreateBackground();

		backgroundImage.transform.SetAsFirstSibling();
	}

	private void CreateBackground()
	{
		backgroundImage = _diContainer.InstantiatePrefab(backgroundPrefab, GameLauncher.Instance.Canvas.transform).GetComponent<Image>();
	}

    private void CreateMenus()
    {
        foreach (var menu in menuPrefabs)
        {
            _menus.Add(_diContainer.InstantiatePrefab(menu, GameLauncher.Instance.MenusParent).GetComponent<BaseMenu>());
        }
    }

    private void HideAllMenus()
    {
        foreach (var menu in _menus)
        {
            menu.Hide();
        }
    }

    public bool TryGetMenuByType<T>(out T menu) where T : BaseMenu
    {
        menu = _menus.FirstOrDefault(x => x.GetType().Equals(typeof(T))) as T;
        if (menu)
        {
            return true;
        }
        return false;
    }

	public void SetBackgroundSprite(Sprite backgroundSprite)
	{
		backgroundImage.sprite = backgroundSprite;
	}
}
