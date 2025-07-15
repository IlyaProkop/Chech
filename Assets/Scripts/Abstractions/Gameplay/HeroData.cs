using UniRx;

public class HeroData
{
    public ReactiveProperty<int> Level { get; set; } = new ReactiveProperty<int>(1);    
  
    public ReactiveProperty<int> Strength { get; set; } = new ReactiveProperty<int>(10);    
   
    public ReactiveProperty<int> Health { get; set; } = new ReactiveProperty<int>(100);

    public ReactiveProperty<bool> IsDirty { get; set; } = new ReactiveProperty<bool>(false);
  
} 