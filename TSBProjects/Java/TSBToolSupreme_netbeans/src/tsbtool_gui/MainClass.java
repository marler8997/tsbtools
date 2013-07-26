/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package tsbtool_gui;

import tsbtoolsupreme.*;

/**
 *
 * @author Chris
 */
public class MainClass {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        
        if( args.length == 0)
        {
            MainGUI.main(args);
        }
        else
        {
            ITecmoTool tool = TecmoToolFactory.GetToolForRom(args[0]);
            try{
            System.out.println(
                tool.GetAll());
            }
            catch( Exception e)
            {
                System.err.println(e.getMessage());
            }
        }
    }
}
