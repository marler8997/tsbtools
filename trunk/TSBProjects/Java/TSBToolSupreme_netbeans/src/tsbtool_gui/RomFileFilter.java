/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package tsbtool_gui;
import java.io.File;

/**
 *
 * @author Chris
 */
public class RomFileFilter extends javax.swing.filechooser.FileFilter
{
    private final String[] okFileExtensions = 
    new String[] {"nes", "smc"};

    @Override
    public boolean accept(File f) 
    {
        String path = f.getAbsolutePath().toLowerCase();
        for(String dude : okFileExtensions )
        {
            if( path.endsWith(dude))
                return true;
        }
        return false;
    }
            
    @Override
    public String getDescription() 
    {
        return ".nes and .smc Files";
    }
  
}
