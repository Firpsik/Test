using Hwdtech;

namespace SpaceBattle;

public class BuildTreeCommand
{
    private readonly ITreeBuildable build;

    public BuildTreeCommand(ITreeBuildable Build)
    {
        build = Build;
    }

    public void Execute()
    {
        build.Tree().ForEach(nums =>
        {
            var node = IoC.Resolve<Dictionary<int, object>>("BuildDecisionTree.Command");
            nums.ForEach(num =>
            {
                node[num] = node.ContainsKey(num) ? node[num] : new Dictionary<int, object>();
                node = (Dictionary<int, object>)node[num];
            });
        });
    }
}