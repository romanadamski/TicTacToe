using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseManager<UIManager>
{
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
		backgroundImage = Instantiate(backgroundPrefab, GameLauncher.Instance.Canvas.transform);
	}

    private void CreateMenus()
    {
        foreach (var menu in menuPrefabs)
        {
            _menus.Add(Instantiate(menu, GameLauncher.Instance.MenusParent));
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
