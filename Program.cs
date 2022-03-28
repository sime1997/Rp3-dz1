
using System;

class node
{
    public int N;
    public string Text=""; 
    public List<node> Lis=new List<node>();
    public Dictionary<string, node> Dict=new Dictionary<string, node>();
    public bool k_N = false;
    public bool k_Text = false;
    public bool k_Lis = false;
    public bool k_Dict = false;

    public node(int n)
    {
        this.N = n;
        this.k_N = true;
    }

    public node(string text)
    {
        Text = text;
        this.k_Text = true;
    }

    public node(List<node> lis)
    {
        Lis = lis;
        this.k_Lis = true;
    }

    public node(Dictionary<string, node> dict)
    {
        Dict = dict;
        this.k_Dict = true;
    }

    

    

    void draw(char[,] polje,int x,int y,int z,int w,int h)
    {
        polje[y, x] = '/';
        polje[y + h - 1, x] = '\\';
        polje[y, x + w - 1] = '\\';
        polje[y + h - 1, x + w - 1] = '/';


        for (int i = 0; i < h; i++)
        {
            if (i != 0 && i != h - 1)
            {
                polje[i + y, x] = '|';
                polje[i + y, x + w - 1] = '|';
                for (int j = 1; j < w - 1; j++)
                    polje[i+y,x+j]= ' ';
            }

            
            else
                for (int j = 1; j < w - 1; j++)
                    polje[y + i, x + j] = '-';

        }
    }


    public void write(char[,] polje, string text, int x, int y, int w, int h)
    {

        

       


        int k =0;
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
            {
                if (k < text.Length)
                {
                    
                   if (text[k] == ' ')
                    {
                       /* for (l = k; l < text.Length; l++)
                        {
                            if (l == ' ' || l == '\n')
                                break;
                        }
                        if (l - k  > w - j)
                            break;*/
                    }


                    polje[y + i + 1, x + j + 1] = text[k++];
                }
            }
                
    }

    void help(char[,] polje, int x, int y, int z , int w , int h )
    {
        int space_x=1, space_y=1;
        x++;
        y++;
        int kontrola = 1;
      
        

        if (this.Dict.ContainsKey("x"))
            {
            if (w - 2 <= this.Dict["x"].N)
                return;
            space_x = this.Dict["x"].N+1;
            x += this.Dict["x"].N;
            }
        
       
        if (this.Dict.ContainsKey("y"))
            {
            if (h - 2 <= this.Dict["y"].N)
                return;
            space_y = this.Dict["y"].N+1;
            y += this.Dict["y"].N;
            }
        


        
        if (this.Dict.ContainsKey("w"))
            {
            if (this.Dict["w"].N>w - space_x-1) 
                 w = w - space_x - 1;
                    
            else
                 w = this.Dict["w"].N;
            }

        else 
            {
            if (80 > w - space_x - 1)
                w =w-space_x - 1;
            else
                w = 80;
            }


        
        if (this.Dict.ContainsKey("h"))
            {
            
            if (this.Dict["h"].N > h - space_y - 1)
                h = h-space_y - 1;
            else
                h = this.Dict["h"].N;
            }
        else 
            {
            if (80 > h - space_y - 1) 
                h=h-space_y - 1;
            else
                y = 11;
           }

        if (w < 2 || h < 2) return;

        if (this.Dict.ContainsKey("z"))
        {
            if (z > this.Dict["z"].N)
                kontrola = 0;
            z = this.Dict["z"].N;
        }
       
        if(kontrola==1)
            this.draw(polje, x, y, z, w, h);


       
        
            if (this.Dict["children"].Text != null && this.Dict["children"].Text != "")
                this.write(polje, this.Dict["children"].Text, x, y, w-2, h-2);
        

        else if (this.Dict["children"].Lis != null)
            {
            foreach (node nd in this.Dict["children"].Lis)

                if (nd.Dict.ContainsKey("z") == false)
                    nd.Dict["z"] = new node(0);
                    
                List<node> sortedList = this.Dict["children"].Lis.OrderBy(o => o.Dict["z"].N).ToList();
            foreach (node nd in sortedList)
            {
             
             nd.help(polje, x, y, z, w, h);
            
            }
        }
        
     }
    
    

    public override string ToString()

    { int w, h,z;
        if (this.Dict.ContainsKey("w") && this.Dict["w"].N<80)
            w=this.Dict["w"].N;
        
        else
            w = 80;

        
        if (this.Dict.ContainsKey("h") && this.Dict["h"].N<11)
            h = this.Dict["w"].N;
        
        else
            h = 11;

        if (this.Dict.ContainsKey("z"))
            z = this.Dict["z"].N;
        else
            z = 0;


        char[,] polje = new char[h, w]; 
        this.draw(polje, 0, 0, 0, w, h);



        if (this.Dict["children"].Text != null && this.Dict["children"].Text != "")
            this.write(polje, this.Dict["children"].Text, 0, 0, w-2, h-2);

        else if (this.Dict["children"].Lis != null)
        {
            foreach (node nd in this.Dict["children"].Lis)

                if (nd.Dict.ContainsKey("z") == false)
                    nd.Dict["z"] = new node(0);

            List<node> sortedList = this.Dict["children"].Lis.OrderBy(o => o.Dict["z"].N).ToList();

            foreach (node nd in sortedList)
                nd.help(polje, 0, 0, z, w, h);
        }

        string output="";
       
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
                output+=polje[i,j];
            output += '\n';
        }
        return output;
     }



    public string pretty_print(int razmak = 0)
    {
        string output = "";
        if (this.k_N)
        {

            output += N.ToString();
            return output;
        }

        else if (this.k_Text)
        {
            output += '"';
            output += Text.ToString();
            output += '"';
            return output;

        }
        else if (this.k_Lis)
        {

            if (!this.Lis.Any())

            {
              
                return "[]";
            }
            output += "[";
            int remember = razmak;
            razmak += 4;
            foreach (node nd in Lis)
            {
               
                output += "\n";
                for (int j = 0; j < razmak; j++)
                    output += " ";
                output += nd.pretty_print(razmak);
                output += ",";

            }
           
            output += "\n";
            for (int i = 0; i < remember; i++)
                output += " ";
            razmak = remember;
            output += "]";
            return output;
        }
        else if (this.k_Dict)
        {
            if (!this.Dict.Any()) return "{}";

           

            output += "{";
            razmak += 4;
            foreach (var item in Dict)
            {
                
                output += "\n";
                for (int i = 0; i < razmak; i++)
                    output += " ";
                output += item.Key;
                output += ":";
                output += " ";
                output += item.Value.pretty_print(razmak);
                output += ",";
            }
            razmak -= 4;
            output += "\n";
            for (int i = 0; i < razmak; i++)
                output += " ";
            output += "}";
            return output;
        }
        else
            return "";
    }
}

class Program
{
    public static void Main(string[] args)
    {
        
    } 
}
