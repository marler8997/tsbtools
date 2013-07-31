/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package tsbtool_gui;

import javax.swing.JApplet;

/**
 *
 * @author Chris
 */
public class TSBToolSupremeApplet extends JApplet
{

    /**
     * Initialization method that will be called after the applet is loaded into
     * the browser.
     */
    public void init()
    {
        // TODO start asynchronous download of heavy resources
        getContentPane().add(new TSBToolAppletPanel());
    }
    // TODO overwrite start(), stop() and destroy() methods
}
