using UnityEngine;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(ResourceScanner))]
[RequireComponent(typeof(BotFactory))]
public class Base : MonoBehaviour
{
    [SerializeField] private TMP_Text _resourceCountLabel;
    [SerializeField] private int _startingResources;
    [SerializeField] private int _botCost;
    
    private ResourceScanner _resourceScanner;
    private BotFactory _botFactory;
    private List<Bot> _availableBots = new();
    private int _resourceCount;

    private void Awake()
    {
        _resourceScanner = GetComponent<ResourceScanner>();
        _botFactory = GetComponent<BotFactory>();

        _resourceCount = _startingResources;
    }

    private void Update()
    {
        GatherResources();
        TryCreateBot();
    }

    public void DepositResource(Bot bot, Resource resource)
    {
        if (resource == null || bot == null)
            return;

        _resourceCount++;

        Destroy(resource.gameObject);

        _availableBots.Add(bot);

        UpdateResourceLabel();
    }

    private void GatherResources()
    {
        if (_resourceScanner.ScannedResourcesCount == 0)
            return;

        Bot bot = GetNextBot();
        Resource resource = GetNextResource();

        if (bot == null || resource == null)
            return;

        DispatchBot(bot, resource);
    }

    private void DispatchBot(Bot bot, Resource resource)
    {
        _availableBots.Remove(bot);
        _resourceScanner.Remove(resource);

        bot.GatherResource(resource);
    }

    private Bot GetNextBot() => GetNextItem(_availableBots);

    private Resource GetNextResource() => GetNextItem(_resourceScanner.ScannedResources);

    private T GetNextItem<T>(List<T> itemList)
    {
        if (itemList.Count == 0)
            return default;

        return itemList[0];
    }

    private void TryCreateBot()
    {
        if (_resourceCount < _botCost)
            return;

        _resourceCount -= _botCost;

        Bot newBot = _botFactory.CreateBot();

        newBot.SetBase(this);
        _availableBots.Add(newBot);

        UpdateResourceLabel();
    }

    private void UpdateResourceLabel()
    {
        _resourceCountLabel.text = _resourceCount.ToString();
    }
}
