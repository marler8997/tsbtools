/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package tsbtool_gui;

import java.awt.Dimension;
import java.awt.Point;
import java.awt.Rectangle;
import java.awt.image.BufferedImage;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.nio.CharBuffer;
import java.util.Arrays;
import java.util.Date;
import java.util.Vector;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import javax.imageio.ImageIO;
import javax.swing.JFileChooser;
import javax.swing.JOptionPane;
import tsbtoolsupreme.ITecmoTool;
import tsbtoolsupreme.TecmoToolFactory;
import javax.swing.filechooser.FileFilter;
import tsbtoolsupreme.InputParser;

/**
 *
 * @author Chris
 */
public class MainGUI extends javax.swing.JFrame {
    		public static String aboutMsg= 
tsbtoolsupreme.MainClass.version +"\n"+
"NEW!!! ->  Double click on a player to bring up the new player editing GUI with that player selected.\n\n"+

"====================BASIC USAGE=================\n"+
"1. Load TSB nes or snes rom.\n"+
"2. View Contents.\n"+
"3. Modify player attributes or schedules.\n"+
"4. Apply to Rom.\n"+
"=============================================\n"+
"This tool was created to make it easier and faster to edit players and schedules in \n"+
"Tecmo Super Bowl (nes version, 32 team nes version, snes TSB1 version). \n"+
"It is intended to be used as a complement to TSBM (from emuware).It does not do \n"+
"everything that TSBM or TSBM 2000 does. It's purpose is to make it easy and fast to \n"+
"modify player names, player attributes, team attributes and season schedules.\n\n"+

"This program can read from standard in or from a file (when executed from command line)\n"+
"View the README to learn how to use it from the command line. To view command line options\n"+
"type 'TSBToolSupreme /?' at the command prompt.\n\n"+

"Use this program at your own risk. TSBToolSupreme creator is not responsible for anyting bad that happens.\n"+
"User takes full responsibility for anything that happens as a result of usung this program.\n"+
"Do not Break copyright laws.\n"+

"This Program is not endorsed or related to the Tecmo video game company.\n"+
"";
    
    private ITecmoTool tool;
    private RomFileFilter nesFilter = new RomFileFilter();
    private boolean mOnWindows = false;

    /**
     * Creates new form MainGUI
     */
    public MainGUI() 
    {
        initComponents();
        //check to see if we are on windows
        // mEolMenuItem.getState 
        if( System.getProperty("os.name").toLowerCase().indexOf("windows") > -1 )
        {
            mOnWindows = true;
            mEolMenuItem.setState(mOnWindows);
        }
        state1();
    }
    
    public static BufferedImage GetImage(String fileName, Object instance) 
    {
        BufferedImage buff = null;
        InputStream is = instance.getClass().getResourceAsStream(fileName);
        try {
            buff = ImageIO.read(is);
        } catch (IOException ex) {
            Logger.getLogger(MainGUI.class.getName()).log(Level.SEVERE, null, ex);
        }
        return buff;
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        mLoadTSBRomButton = new javax.swing.JButton();
        mSaveDataButton = new javax.swing.JButton();
        mViewContentsButton = new javax.swing.JButton();
        mLoadDataButton = new javax.swing.JButton();
        mApplyToRomButton = new javax.swing.JButton();
        jScrollPane1 = new javax.swing.JScrollPane();
        mTextArea = new javax.swing.JTextArea();
        mMenuBar = new javax.swing.JMenuBar();
        jMenu1 = new javax.swing.JMenu();
        mLoadRomMenuItem = new javax.swing.JMenuItem();
        mLoadDataMenuItem = new javax.swing.JMenuItem();
        mApplyToRomMenuItem = new javax.swing.JMenuItem();
        mGetBytesMenuItem = new javax.swing.JMenuItem();
        jMenu2 = new javax.swing.JMenu();
        mPlaybookMenuItem = new javax.swing.JCheckBoxMenuItem();
        mOffensiveFormationsMenuItem = new javax.swing.JCheckBoxMenuItem();
        mOffensivePrefMenuItem = new javax.swing.JCheckBoxMenuItem();
        mEolMenuItem = new javax.swing.JCheckBoxMenuItem();
        mShowColorsMenuItem = new javax.swing.JCheckBoxMenuItem();
        mEditPlayersItem = new javax.swing.JMenuItem();
        mEditTeamsItem = new javax.swing.JMenuItem();
        mDleteCommasItem = new javax.swing.JMenuItem();
        jMenu3 = new javax.swing.JMenu();
        jMenu4 = new javax.swing.JMenu();
        mAboutItem = new javax.swing.JMenuItem();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setIconImage( MainGUI.GetImage("/tsbtool_gui/icons/49ers.png", this));
        setMinimumSize(new java.awt.Dimension(688, 200));
        setPreferredSize(new java.awt.Dimension(764, 536));

        mLoadTSBRomButton.setText("<HTML>\nLoad TSB ROM<br>\n(nes or snes)\n</HTML>");
        mLoadTSBRomButton.setHorizontalAlignment(javax.swing.SwingConstants.LEFT);
        mLoadTSBRomButton.setVerticalAlignment(javax.swing.SwingConstants.TOP);
        mLoadTSBRomButton.setVerticalTextPosition(javax.swing.SwingConstants.TOP);
        mLoadTSBRomButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mLoadTSBRomButtonActionPerformed(evt);
            }
        });

        mSaveDataButton.setText("Save Data");
        mSaveDataButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mSaveDataButtonActionPerformed(evt);
            }
        });

        mViewContentsButton.setText("View Contents");
        mViewContentsButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mViewContentsButtonActionPerformed(evt);
            }
        });

        mLoadDataButton.setText("Load Data");
        mLoadDataButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mLoadDataButtonActionPerformed(evt);
            }
        });

        mApplyToRomButton.setText("Apply To Rom");
        mApplyToRomButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mApplyToRomButtonActionPerformed(evt);
            }
        });

        mTextArea.setColumns(20);
        mTextArea.setFont(new java.awt.Font("Courier New", 0, 12)); // NOI18N
        mTextArea.setRows(5);
        mTextArea.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                mTextAreaMouseClicked(evt);
            }
        });
        jScrollPane1.setViewportView(mTextArea);

        jMenu1.setText("File");

        mLoadRomMenuItem.setText("Load TSB ROM");
        mLoadRomMenuItem.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mLoadRomMenuItemActionPerformed(evt);
            }
        });
        jMenu1.add(mLoadRomMenuItem);

        mLoadDataMenuItem.setText("Load Data");
        mLoadDataMenuItem.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mLoadDataMenuItemActionPerformed(evt);
            }
        });
        jMenu1.add(mLoadDataMenuItem);

        mApplyToRomMenuItem.setText("Apply To ROM");
        mApplyToRomMenuItem.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mApplyToRomMenuItemActionPerformed(evt);
            }
        });
        jMenu1.add(mApplyToRomMenuItem);

        mGetBytesMenuItem.setText("Get Bytes");
        mGetBytesMenuItem.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mGetBytesMenuItemActionPerformed(evt);
            }
        });
        jMenu1.add(mGetBytesMenuItem);

        mMenuBar.add(jMenu1);

        jMenu2.setText("View");

        mPlaybookMenuItem.setSelected(true);
        mPlaybookMenuItem.setText("Show Playbooks");
        jMenu2.add(mPlaybookMenuItem);

        mOffensiveFormationsMenuItem.setSelected(true);
        mOffensiveFormationsMenuItem.setText("Show Offensive Formations");
        jMenu2.add(mOffensiveFormationsMenuItem);

        mOffensivePrefMenuItem.setSelected(true);
        mOffensivePrefMenuItem.setText("Show Offensive preference");
        jMenu2.add(mOffensivePrefMenuItem);

        mEolMenuItem.setSelected(true);
        mEolMenuItem.setText("EOL (Windows Style) CR LF | CR ");
        jMenu2.add(mEolMenuItem);

        mShowColorsMenuItem.setText("Show Colors");
        jMenu2.add(mShowColorsMenuItem);

        mEditPlayersItem.setText("Edit Players");
        mEditPlayersItem.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mEditPlayersItemActionPerformed(evt);
            }
        });
        jMenu2.add(mEditPlayersItem);

        mEditTeamsItem.setText("Edit Teams");
        mEditTeamsItem.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mEditTeamsItemActionPerformed(evt);
            }
        });
        jMenu2.add(mEditTeamsItem);

        mDleteCommasItem.setText("Delete Trailing Commas");
        mDleteCommasItem.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mDleteCommasItemActionPerformed(evt);
            }
        });
        jMenu2.add(mDleteCommasItem);

        mMenuBar.add(jMenu2);

        jMenu3.setText("Search");
        mMenuBar.add(jMenu3);

        jMenu4.setText("About");

        mAboutItem.setText("About TSBTool");
        mAboutItem.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mAboutItemActionPerformed(evt);
            }
        });
        jMenu4.add(mAboutItem);

        mMenuBar.add(jMenu4);

        setJMenuBar(mMenuBar);

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(mLoadTSBRomButton, javax.swing.GroupLayout.PREFERRED_SIZE, 122, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(mViewContentsButton, javax.swing.GroupLayout.PREFERRED_SIZE, 110, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(mSaveDataButton, javax.swing.GroupLayout.PREFERRED_SIZE, 110, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 95, Short.MAX_VALUE)
                .addComponent(mLoadDataButton, javax.swing.GroupLayout.PREFERRED_SIZE, 110, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(mApplyToRomButton, javax.swing.GroupLayout.PREFERRED_SIZE, 110, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(47, 47, 47))
            .addComponent(jScrollPane1, javax.swing.GroupLayout.Alignment.TRAILING)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addComponent(jScrollPane1, javax.swing.GroupLayout.DEFAULT_SIZE, 284, Short.MAX_VALUE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                        .addComponent(mSaveDataButton, javax.swing.GroupLayout.PREFERRED_SIZE, 41, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addComponent(mViewContentsButton, javax.swing.GroupLayout.PREFERRED_SIZE, 41, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addComponent(mLoadDataButton, javax.swing.GroupLayout.PREFERRED_SIZE, 41, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addComponent(mApplyToRomButton, javax.swing.GroupLayout.PREFERRED_SIZE, 41, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addComponent(mLoadTSBRomButton, javax.swing.GroupLayout.PREFERRED_SIZE, 41, javax.swing.GroupLayout.PREFERRED_SIZE)))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void mViewContentsButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mViewContentsButtonActionPerformed
        ViewContents();
    }//GEN-LAST:event_mViewContentsButtonActionPerformed

    private void mLoadTSBRomButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mLoadTSBRomButtonActionPerformed
        LoadRom();
    }//GEN-LAST:event_mLoadTSBRomButtonActionPerformed

    private void mSaveDataButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mSaveDataButtonActionPerformed
        SaveData();
    }//GEN-LAST:event_mSaveDataButtonActionPerformed

    private void mLoadDataButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mLoadDataButtonActionPerformed
        LoadData();
    }//GEN-LAST:event_mLoadDataButtonActionPerformed

    private void mApplyToRomButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mApplyToRomButtonActionPerformed
        ApplyToRom();
    }//GEN-LAST:event_mApplyToRomButtonActionPerformed

    private Date m_LastTime = new Date();
    private void mTextAreaMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_mTextAreaMouseClicked
        Date now = new Date();
        //System.out.printf(" called at %d Difference = %d \n",+now.getTime(),  now.getTime() - m_LastTime.getTime());
        if( now.getTime() - m_LastTime.getTime() < 1000  ) // make sure we're clicking within .5 seconds
        {
            try{ DoubleClicked();}
            catch(Exception ex ){
                ex.printStackTrace();
            }
        }
        m_LastTime = now;
    }//GEN-LAST:event_mTextAreaMouseClicked

    private void mLoadRomMenuItemActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mLoadRomMenuItemActionPerformed
        LoadRom();
    }//GEN-LAST:event_mLoadRomMenuItemActionPerformed

    private void mLoadDataMenuItemActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mLoadDataMenuItemActionPerformed
        LoadData();
    }//GEN-LAST:event_mLoadDataMenuItemActionPerformed

    private void mApplyToRomMenuItemActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mApplyToRomMenuItemActionPerformed
        ApplyToRom();
    }//GEN-LAST:event_mApplyToRomMenuItemActionPerformed

    private void mGetBytesMenuItemActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mGetBytesMenuItemActionPerformed
        GetBytes();
    }//GEN-LAST:event_mGetBytesMenuItemActionPerformed

    private void mEditPlayersItemActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mEditPlayersItemActionPerformed
        try {
            EditPlayer();
        } catch (IOException ex) {
            Logger.getLogger(MainGUI.class.getName()).log(Level.SEVERE, null, ex);
        }
    }//GEN-LAST:event_mEditPlayersItemActionPerformed

    private void mEditTeamsItemActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mEditTeamsItemActionPerformed
        ModifyTeams();
    }//GEN-LAST:event_mEditTeamsItemActionPerformed

    private void mDleteCommasItemActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mDleteCommasItemActionPerformed
        String txt = InputParser.DeleteTrailingCommas( mTextArea.getText() );
        SetText( txt);
    }//GEN-LAST:event_mDleteCommasItemActionPerformed

    private void mAboutItemActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mAboutItemActionPerformed
        JOptionPane.showMessageDialog(this, MainGUI.aboutMsg);
    }//GEN-LAST:event_mAboutItemActionPerformed

    private void GetBytes()
    {
        if( tool != null && tool.getOutputRom() != null )
        {
            String fileName = GetFileName(null, false);
            if( fileName != null )
            {
                String result;
                try {
                    result = tsbtoolsupreme.MainClass.GetLocations(fileName, tool.getOutputRom());
                    TextDisplayDialog dlg = new TextDisplayDialog();
                    dlg.setText(result);
                    dlg.setTitle("Results from '"+ fileName + "'");
                    dlg.setVisible(true);

                } catch (NumberFormatException ex) {
                    Logger.getLogger(MainGUI.class.getName()).log(Level.SEVERE, null, ex);
                } catch (IOException ex) {
                    Logger.getLogger(MainGUI.class.getName()).log(Level.SEVERE, null, ex);
                }
                
//                RichTextDisplay disp = new RichTextDisplay();
//                disp.ContentBox.Font = richTextBox1.Font;
//                disp.ContentBox.Text = result;
//                disp.Text = "Results from '"+ fileName + "'";
//                disp.Show();
            }
        }
        else
        {
            JOptionPane.showMessageDialog(this, "Please load a ROM first", "Error!!", JOptionPane.ERROR_MESSAGE);
        }
    }
    private void DoubleClicked() throws IOException
    {
        String line =  GetLine (mTextArea.getSelectionStart());
        if( line == null )
                return;
        if( line.indexOf("TEAM") > -1 || line.indexOf("PLAYBOOK") > -1 )
        {
            ModifyTeams();
        }
        else if( line.indexOf("COLORS") > -1 )
        {
            //ModifyColors(); // java TODO:
        }
        else
            EditPlayer();
    }
    
    /// <summary>
    /// returns the line that linePosition falls on
    /// </summary>
    /// <param name="textPosition"></param>
    /// <returns></returns>
    private String GetLine(int textPosition)
    {
        String ret = null;
        String text = mTextArea.getText();
        if( textPosition < text.length() )
        {
            int i=0;
            int lineStart = 0;
            int posLen = 0;
            for(i = textPosition; i > 0; i-- )
            {
                if(text.charAt(i) == '\n')
                {
                    lineStart = i+1;
                    break;
                }
            }
            i = lineStart;
            if( i < text.length() )
            {
                char current = text.charAt(i);
                while( i < text.length()-1 /*&& current != ' ' && 
                current != ',' */ && current != '\n')
                {
                    posLen++;
                    i++;
                    current = text.charAt(i);
                }
                ret = text.substring(lineStart, lineStart + posLen);
            }
        }
        return ret;
    }

    
    private void EditPlayer() throws IOException
    {
        int pos       = mTextArea.getSelectionStart();
        int lineStart = 0;
        int posLen    = 0;
        String position = "QB1";
        String team   = "bills";

        String text = mTextArea.getText();
        if( pos > 0 && pos < text.length() )
        {
            int i =0;
            for(i = pos; i > 0; i-- )
            {
                if(text.charAt(i) == '\n')
                {
                    lineStart = i+1;
                    break;
                }
            }
            i = lineStart;
            char current =text.charAt(i);
            while( i < text.length() && current != ' ' && 
                    current != ',' && current != '\n')
            {
                posLen++;
                i++;
                current = text.charAt(i);
            }
            position = text.substring(lineStart, lineStart + posLen);

            team = GetTeam(pos);
            ModifyPlayers(team, position);
        }
    }
    
    private void ModifyPlayers(String team, String position) throws IOException
    {
        AttributeForm form   = new AttributeForm();
        form.setData(mTextArea.getText());
        form.setCurrentTeam( team);
        form.setCurrentPosition(position);
        form.setAutoUpdatePlayersUI(true);

        form.setModal(true);
        form.setVisible(true);  
        
        if( !form.getCanceled() )
        {
            int spot2 = mTextArea.getSelectionStart();
            String newText = form.getData(); 
            SetText( newText);
            if( newText.length() > spot2 )
            {
                mTextArea.setSelectionStart(spot2);
            }
        }
        form.dispose();
    }
    
    private String GetTeam(int textPosition)
    {
        String team = "bills";
        Pattern r = Pattern.compile("TEAM\\s*=\\s*([a-zA-Z49]+)");
        
        Matcher mc = r.matcher(mTextArea.getText());

        while(mc.find())
        {
            if(mc.start() > textPosition )
            {
                break;
            }
            team = mc.group(1);
        }
        return team;
    }

    private void ModifyTeams()
    {
            String team = GetTeam(mTextArea.getSelectionStart());
            ModifyTeams(team);
    }

    private void ModifyTeams(String team)
    {
            ModifyTeamForm form = new ModifyTeamForm(this, true);
            form.setData( mTextArea.getText());
            form.setCurrentTeam(team);
            form.setVisible(true);

            if( !form.getCanceled() )
            {
                int index = mTextArea.getSelectionStart();
                Point pt = mTextArea.getCaret().getMagicCaretPosition();
                Rectangle rect = new Rectangle(pt, new Dimension(1, 10));

                SetText( form.getData());
                if( mTextArea.getText().length() > index)
                {
                    mTextArea.setSelectionStart(index);
                    mTextArea.scrollRectToVisible(rect);
                }
            }
            form.dispose();
    }
    
    /// <summary>
    /// Returns filename on 'OK' null on 'cancel'.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    private String GetFileName(FileFilter filter, boolean saveFileDlg)
    {
        String ret=null;
        //FileDialog dlg;
        JFileChooser dlg = new JFileChooser(".");
        dlg.setFileSelectionMode(JFileChooser.FILES_ONLY);
        int dialogResult = JFileChooser.CANCEL_OPTION;
        if( saveFileDlg )
        {
            dlg.setFileFilter(filter);
            dialogResult = dlg.showSaveDialog(this);
        }
        else
        {
            dlg.setFileFilter(filter);
            dialogResult = dlg.showOpenDialog(this);
        }
        if( dialogResult == JFileChooser.APPROVE_OPTION)
        {
            ret = dlg.getSelectedFile().getAbsolutePath();
        }
        return ret;
    }
    
    private void LoadRom()
    {
        String filename = GetFileName(nesFilter, false);
        
        if( filename != null)
            tool = TecmoToolFactory.GetToolForRom( filename );
        
        if(filename != null && tool != null)
        {
            if( tool.getOutputRom() != null )
            {
                state2();
                UpdateTitle(filename);
            }
            else if( tool.Init(filename))
            {
                state2();
                UpdateTitle(filename);
            }
            else
                state1();
        }
    }
    
    private void SaveData()
    {
        String filename = GetFileName(null,true);
        if(filename != null)
        {
            try
            {
                //StreamWriter sw = new StreamWriter(filename);
                FileOutputStream fos = new FileOutputStream(filename); 
                OutputStreamWriter sw = new OutputStreamWriter(fos, "UTF-8");
                String text = mTextArea.getText();
                if( mEolMenuItem.getState() )
                { // if we're on windows
                    text = text.replace("\r\n", "\n");
                    text = text.replace("\n","\r\n");
                }
                sw.write( text );
                sw.close();
            }
            catch(Exception e)
            {
                ShowError(String.format("ERROR! Could not save to file %s\n%s",
                        filename,e.getMessage()));
            }
        }
    }
    private void LoadData()
    {
        String fileName = GetFileName(null,false);
	LoadDataFile(fileName);
    }
    
    private void LoadDataFile(String fileName)
    {
        if(fileName != null && fileName.length() > 0)
        {
            try
            {
                BufferedReader br = new BufferedReader(new FileReader(fileName));
                //StreamReader sr = new StreamReader(fileName);
                StringBuilder builder = new StringBuilder(1000);
                String chunk = "";
                while( (chunk = br.readLine()) != null)
                {
                    builder.append(chunk);
                    builder.append("\n");
                }
                br.close();
                SetText(builder.toString());
            }
            catch(Exception ee)
            { 
                ShowError("Error Reading file" +fileName);
            }
        }
    }
    
    private void ApplyToRom()
    {
        String saveToFilename = GetFileName(nesFilter, true);
        String[] lines = mTextArea.getText().split("\n");
        Vector<String> liness = new Vector<String>(Arrays.asList(lines));
        InputParser parser = new InputParser(tool);
        parser.ProcessLines(liness);
        tool.SaveRom(saveToFilename);
        UpdateTitle(saveToFilename);
    }
    
    private void ViewContents() 
    {
        tool.setShowOffPref(this.mOffensivePrefMenuItem.getState());
        tsbtoolsupreme.TecmoTool.ShowPlaybook = mPlaybookMenuItem.getState();
        tsbtoolsupreme.TecmoTool.ShowTeamFormation = mOffensiveFormationsMenuItem.getState();
        tsbtoolsupreme.TecmoTool.ShowColors = mShowColorsMenuItem.getState();
        
        String msg = 
                        "# Double click on a team or player to bring up the All new Player/Team editing GUI.\r\n"+
                    "# Select (Show Colors) menu Item (under view Menu) to enable listing of team colors.\r\n"+
                    "#  Double Click on a 'COLORS' line to edit team COLORS.\r\n";
        StringBuilder text = new StringBuilder(70*1024);
        try {
            text.append(msg);
            text.append(tool.GetKey());

            text.append(tool.GetAll());

            text.append(tool.GetSchedule());
            SetText(text.toString());
        } catch (Exception ex) {
            Logger.getLogger(MainGUI.class.getName()).log(Level.SEVERE, null, ex);
        }
            //mTextArea.setSelectionStart(0);
            //mTextArea.SelectionLength = msg.Length-2;
            //mTextArea.SelectionColor = Color.Magenta;
            mTextArea.setSelectionStart(0);
            //mTextArea.setSelectionLength(0);

    }
    public void ShowError(String error)
    {
        JOptionPane.showMessageDialog(this, error, "Eror!", JOptionPane.ERROR_MESSAGE);
    }
    
    private void SetText( String text )
    {
        this.mTextArea.setText(text);
         
          mTextArea.setCaretPosition(0);
//        mTextArea.SelectAll();
//        mTextArea.SelectionColor = Color.Black;
//        mTextArea.SelectionLength = 0;
//        mTextArea.SelectionStart = 0;
    }
    private void state1()
    {
        mViewContentsButton.setEnabled(false);
        //viewTSBContentsMenuItem.Enabled=false;
        mApplyToRomButton.setEnabled(false);
        //applyToRomMenuItem.Enabled=false;
    }

    private void state2()
    {
        mViewContentsButton.setEnabled(true);
        //viewTSBContentsMenuItem.Enabled=true;
        mApplyToRomButton.setEnabled(true);
        //applyToRomMenuItem.Enabled=true;
    }
    private void UpdateTitle(String filename )
    {
        if( filename != null )
        {
            String fn = filename;
            String pathSep = java.lang.System.getProperty("path.separator");
            int index = filename.lastIndexOf(pathSep)+1;
            if( index > 0 )
            {
                fn = filename.substring(index);
            }
            if( fn.length() > 4 )
                    this.setTitle( String.format("TSBTool Supreme   '%s' Loaded", fn));
            String type = "  (normal nes file)";
            if( this.tool.getClass().toString().indexOf("CXRomTSBTool" ) > -1)
                type = "   (32 team ROM file)";
            else if( this.tool.getClass().toString().indexOf("SNES_TecmoTool") > -1 )
            {
                type = "   (SNES TSB1 ROM file)";
            }
            this.setTitle(this.getTitle()+type);
        }
    }
    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(MainGUI.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(MainGUI.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(MainGUI.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(MainGUI.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new MainGUI().setVisible(true);
            }
        });
    }
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JMenu jMenu1;
    private javax.swing.JMenu jMenu2;
    private javax.swing.JMenu jMenu3;
    private javax.swing.JMenu jMenu4;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JMenuItem mAboutItem;
    private javax.swing.JButton mApplyToRomButton;
    private javax.swing.JMenuItem mApplyToRomMenuItem;
    private javax.swing.JMenuItem mDleteCommasItem;
    private javax.swing.JMenuItem mEditPlayersItem;
    private javax.swing.JMenuItem mEditTeamsItem;
    private javax.swing.JCheckBoxMenuItem mEolMenuItem;
    private javax.swing.JMenuItem mGetBytesMenuItem;
    private javax.swing.JButton mLoadDataButton;
    private javax.swing.JMenuItem mLoadDataMenuItem;
    private javax.swing.JMenuItem mLoadRomMenuItem;
    private javax.swing.JButton mLoadTSBRomButton;
    private javax.swing.JMenuBar mMenuBar;
    private javax.swing.JCheckBoxMenuItem mOffensiveFormationsMenuItem;
    private javax.swing.JCheckBoxMenuItem mOffensivePrefMenuItem;
    private javax.swing.JCheckBoxMenuItem mPlaybookMenuItem;
    private javax.swing.JButton mSaveDataButton;
    private javax.swing.JCheckBoxMenuItem mShowColorsMenuItem;
    private javax.swing.JTextArea mTextArea;
    private javax.swing.JButton mViewContentsButton;
    // End of variables declaration//GEN-END:variables


}