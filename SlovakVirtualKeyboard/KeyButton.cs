
namespace SlovakVirtualKeyboard
{
    struct SimpleKey
    {
        public char firstSymbol;
        public char secondSymbol;
        public SimpleKey(char firstSymbol, char secondSymbol)
        {
            this.firstSymbol = firstSymbol;
            this.secondSymbol = secondSymbol;
        }
        
    }

    struct СomplexKey
    {
        public char firstSymbol;
        public char secondSymbol;
        public char thirdSymbol;
        public char fourthSymbol;
        public СomplexKey(char firstSymbol, char secondSymbol, char thirdSymbol, char fourthSymbol)
        {
            this.firstSymbol = firstSymbol;
            this.secondSymbol = secondSymbol;
            this.thirdSymbol = thirdSymbol;
            this.fourthSymbol = fourthSymbol;
        }
    }   
}
