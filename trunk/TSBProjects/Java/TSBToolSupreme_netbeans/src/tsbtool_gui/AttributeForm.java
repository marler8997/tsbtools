/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package tsbtool_gui;

import java.awt.Image;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.io.InputStream;
import java.net.URL;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import javax.imageio.ImageIO;
import javax.swing.*;
import tsbtoolsupreme.InputParser;
import tsbtoolsupreme.TSBPlayer;
/**
 *
 * @author Chris
 */
public class AttributeForm extends javax.swing.JDialog 
{
    private JComboBox[]     m_Attributes = null;
    private JSpinner[]      m_SimAttrs   = null;
    private InputParser     m_Parser = null;
    boolean m_DoneInit = false;
    StateEnum m_CurrentState = StateEnum.QB;
    private int m_ImageNumber = 0x00;
    
    /**
     * Creates new form AttributeForm
     */
    public AttributeForm() 
    {
        initComponents();
        mJerseyNumberUpDown.setModel(new SpinnerNumberModel(0,0,99,1));
        
        m_Parser = new InputParser();
        m_Attributes = new JComboBox[8];
        m_Attributes[0] = m_RSBox;
        m_Attributes[1] = m_RPBox;
        m_Attributes[2] = m_MSBox;
        m_Attributes[3] = m_HPBox;
        m_Attributes[4] = m_PS_BC_PI_KABox;
        m_Attributes[5] = m_PC_REC_QU_KABox;
        m_Attributes[6] = m_ACCBox;
        m_Attributes[7] = m_APBBox;

        m_SimAttrs = new JSpinner[4];
        m_SimAttrs[0] = m_Sim1UpDown;
        m_SimAttrs[1] = m_Sim2UpDown;
        m_SimAttrs[2] = m_Sim3UpDown;
        m_SimAttrs[3] = m_Sim4UpDown;
        
        m_DoneInit = true;
        setCurrentState(StateEnum.QB);
        
    }

    private boolean mCanceled = false;
    
    public boolean getCanceled(){
        return mCanceled;
    }
    private String Data ="";
    
    public void setData(String stuff) throws IOException {
        Data = stuff;
        SetupTeams();
        SetCurrentPlayer();
    }
    
    public String getData(){
        return Data;
    }
            
    
    private String CurrentTeam="";
    
    public void setCurrentTeam(String team)
    {
        CurrentTeam = team;
        int index = MainGUI.ComboBoxIndexOf(mTeamsComboBox, team);
        if(index > -1 )
        {
            mTeamsComboBox.setSelectedIndex(index);
        }
    }
    
    private String CurrentPosition ="";
    
    public void setCurrentPosition(String position)
    {
        CurrentPosition = position;
        int index = MainGUI.ComboBoxIndexOf(mPositionComboBox, position);
        if(index > -1 )
        {
            mPositionComboBox.setSelectedIndex(index);
        }
    }
    
    private boolean AutoUpdatePlayersUI = true;
    
    public void setAutoUpdatePlayersUI(boolean b)
    {
        AutoUpdatePlayersUI = b;
    }
    
    private void SetupTeams()
    {
        Pattern teamRegex = Pattern.compile("TEAM\\s*=\\s*([a-z0-9]+)");
        Matcher mc = teamRegex.matcher(Data);

        //mTeamsComboBox.Items.Clear();
        DefaultComboBoxModel model = new DefaultComboBoxModel();
        String team;
        while( mc.find())
        {
            team = mc.group(1) ;
            model.addElement(team);
        }
        mTeamsComboBox.setModel(model);
        if( model.getSize() > 0 )
            mTeamsComboBox.setSelectedIndex(0);
    }
    
    /// <summary>
    /// Updates the GUI with the current player.
    /// </summary>
    private void SetCurrentPlayer() throws IOException
    {
        if( mTeamsComboBox.getSelectedItem() != null )
        {
            String team       = mTeamsComboBox.getSelectedItem().toString();
            String position   = mPositionComboBox.getSelectedItem().toString();
            String playerData = GetPlayerString(team, position);
            if( playerData != null )
                SetPlayerData(playerData);
        }
    }
    
    
    /// <summary>
    /// Updates the GUI with the player 'line' passed.
    /// </summary>
    /// <param name="playerLine"></param>
    private void SetPlayerData(String playerLine) throws IOException
    {
        String fName = m_Parser.GetFirstName(playerLine);
        String lName = m_Parser.GetLastName(playerLine);
        int face = m_Parser.GetFace(playerLine);
        int jerseyNumber = m_Parser.GetJerseyNumber(playerLine);
        int[] attrs = m_Parser.GetInts(playerLine);
        int[] simData = m_Parser.GetSimVals(playerLine);

        mFirstNameTextBox.setText( fName);
        mLastNameTextBox.setText( lName);
        m_ImageNumber = face;
        if( jerseyNumber > -1 && jerseyNumber < 0x100)
            mJerseyNumberUpDown.setValue( Integer.parseInt(String.format("%x", jerseyNumber)));

        if( attrs != null )
        {
            int attrIndex = 0;
            for(int i = 0; i < attrs.length && i < m_Attributes.length; i++)
            {
                attrIndex = AttrIndex(attrs[i]+"");
                if( attrIndex > -1 )
                    m_Attributes[i].setSelectedIndex(attrIndex);
            }
        }
        if( simData != null)
        {
            for( int i =0; i < simData.length; i++)
            {
                m_SimAttrs[i].setValue( Integer.parseInt(simData[i]+""));
            }
        }
        if( jerseyNumber > -1 && jerseyNumber < 0x100)
        {
                mJerseyNumberUpDown.setValue(Integer.parseInt(String.format("%x", jerseyNumber)));
                ShowCurrentFace();
        }
    }
    
    /// <summary>
    /// Gets the proper face for a given player, and updates the displaying image.
    /// </summary>
    private void ShowCurrentFace() 
    {
        if( m_DoneInit )
        {
            String file = "/tsbtool_gui/facepackage/"+String.format("%02x.BMP",m_ImageNumber).toUpperCase();
            //new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/facepackage/00.png")
            Image  face = GetImage(file);
            if( face != null )
            {
                mFaceBox.setIcon( new ImageIcon( face));
                m_FaceLabel.setText(String.format("%02x",m_ImageNumber).toUpperCase());
            }
            else
                JOptionPane.showMessageDialog(this, "Problem with "+file);
            AutoUpdateRoster();
        }
    }
    
    public  BufferedImage GetImage(String fileName) 
    {
        BufferedImage buff = null;
        InputStream is = getClass().getResourceAsStream(fileName);
        if(is == null )
        {
            URL url = getClass().getResource(fileName);
        }
        //getClass().getResource("/img/foo.jpg") (note the leading /), or 
        //getClass().getClassLoader().getResource("img/foo.jpg").
        try {
            buff = ImageIO.read(is);
        } catch (IOException ex) {
            Logger.getLogger(AttributeForm.class.getName()).log(Level.SEVERE, null, ex);
        }
        return buff;
    }
    
    
    private void AutoUpdateRoster()
    {
        if( m_DoneInit && AutoUpdatePlayersUI )
        {
            ReplacePlayer();
        }
    }
    
    /// <summary>
    /// Replaces the current player with the values specified.
    /// </summary>
    private void ReplacePlayer()
    {
        String oldPlayer = GetPlayerString(mTeamsComboBox.getSelectedItem().toString(),
            mPositionComboBox.getSelectedItem().toString());
        if( oldPlayer == null )
            return;

        String newPlayer = GetPlayerString_UI();
        String team = mTeamsComboBox.getModel().getElementAt(mTeamsComboBox.getSelectedIndex()).toString();
        ReplacePlayer(team, oldPlayer, newPlayer);
    }

    private void ReplacePlayer(String team, String oldPlayer, String newPlayer)
    {
            int nextTeamIndex = -1;
            int currentTeamIndex= -1;
            String nextTeam    = null;

            Pattern findTeamRegex = Pattern.compile("TEAM\\s*=\\s*"+team);

            Matcher m = findTeamRegex.matcher(Data);
            if( !m.find() )
                    return;

            //currentTeamIndex = m.group(1).Index;
            currentTeamIndex = m.start();

            int test =MainGUI.ComboBoxIndexOf(mTeamsComboBox, team);
            int modelSize = mTeamsComboBox.getModel().getSize();
 
            if( test != modelSize - 1 )
            {
                nextTeam      = String.format("TEAM\\s*=\\s*%s",mTeamsComboBox.getModel().getElementAt(test+1));
                Pattern nextTeamRegex = Pattern.compile(nextTeam);
                Matcher nt = nextTeamRegex.matcher(Data);
                if( nt.find() )
                     nextTeamIndex = nt.start();
            }
            if( nextTeamIndex < 0)
                nextTeamIndex = Data.length();

            int playerIndex = Data.indexOf(oldPlayer,currentTeamIndex);
            if( playerIndex > -1 )
            {
                int endLine     = Data.indexOf('\n',playerIndex);
                String start    = Data.substring(0,playerIndex);
                String last     = Data.substring(endLine);

                StringBuilder tmp = new StringBuilder(Data.length() + 200);
                tmp.append(start);
                tmp.append(newPlayer);
                tmp.append(last);

                Data = tmp.toString();
                //Data = start + newPlayer + last;
            }
            else
            {
                    String error = String.format(
"An error occured looking up playern"+
"     '%s'\n"+
"Please verify that this player's attributes are correct.", oldPlayer);
                    JOptionPane.showMessageDialog(this, error);
            }
    }
    
    /// <summary>
    /// Returns the text representation of what the GUI is presenting.
    /// </summary>
    /// <returns></returns>
    private String GetPlayerString_UI()
    {
        String ret = GetPlayerString_UI(mPositionComboBox.getSelectedItem().toString());
        return ret;
    }

    /// <summary>
    /// Returns the text representation of what the GUI is presenting.
    /// </summary>
    /// <returns></returns>
    public String GetPlayerString_UI(String position)
    {
            StringBuilder sb = new StringBuilder(60);
            sb.append(position);
            sb.append(", ");
            sb.append(mFirstNameTextBox.getText());
            sb.append(" ");
            sb.append(mLastNameTextBox.getText());
            sb.append(", ");
            sb.append(String.format("Face=0x%x, #%d, ", 
                    m_ImageNumber, mJerseyNumberUpDown.getValue()));
            // attrs
            for( int i = 0; i < m_Attributes.length; i++)
            {
                if( m_Attributes[i].isEnabled())
                {
                    sb.append(m_Attributes[i].getSelectedItem().toString());
                    sb.append(", ");
                }
            }
            //sim attrs
            if( m_SimAttrs[0].isEnabled())
                sb.append("[");
            for(int i = 0; i < m_SimAttrs.length; i++)
            {
                if( m_SimAttrs[i].isEnabled() )
                {
                    sb.append(m_SimAttrs[i].getValue());
                    sb.append(", ");
                }
            }
            if( m_SimAttrs[0].isEnabled())
            {
                sb.delete(sb.length() -2, sb.length());
                sb.append(" ]");
            }

            String ret = sb.toString();
            char[] chars = {' ', ',' };
            ret = ret.trim();
            if( ret.endsWith(","))
            {
                ret = ret.substring(0, ret.length()-2); // TODO: java test this
            }
            return ret;
    }

    /// <summary>
    /// Returns the index of 'val'.
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    private int AttrIndex(String val)
    {
        int ret = MainGUI.ComboBoxIndexOf(m_Attributes[0],val);
        return ret;
    }    
    
    /// <summary>
    /// Gets a player 'line' from Data from 'team' playing 'position'.
    /// </summary>
    /// <param name="team"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    private String GetPlayerString( String team, String position )
    {
        String pattern = "TEAM\\s*=\\s*"+team;
        Pattern findTeamRegex = Pattern.compile(pattern);
        Matcher m = findTeamRegex.matcher(Data);
        if( m.find() )
        {
            int teamIndex = m.start();// m.Index;
            //int teamIndex = Data.IndexOf("TEAM = "+team);
            if( teamIndex == -1 )
                return null;
            //int playerIndex = Data.IndexOf("\n"+position+",", teamIndex);
            int playerIndex = -1;
            Pattern endLineRegex = Pattern.compile(String.format("\n\\s*%s\\s*,",position));
            Matcher eol = endLineRegex.matcher(Data);
            if( eol.find(teamIndex) )
                playerIndex = eol.start();
            playerIndex++;

            if( playerIndex == 0 )
                return null;
            int lineEnd = Data.indexOf("\n",playerIndex);
            String playerLine = Data.substring(playerIndex, lineEnd);
            return playerLine;
        }
        return null;
    }
//    private SpinnerNumberModel m_15Max = new SpinnerNumberModel(0, 0, 15, 1);
//    private SpinnerNumberModel m_255Max = new SpinnerNumberModel(0, 0, 255, 1);
    
    private void setCurrentState(StateEnum state)
    {
        m_CurrentState = state;
        m_DoneInit = false;
        switch(m_CurrentState)
        {
            case QB:
                    m_A1Label.setText( "PS");
                    m_A2Label.setText( "PC");
                    m_A3Label.setText( "ACC");
                    m_A4Label.setText( "APB");
                    m_Sim1Label.setText( "Sim Run");
                    m_Sim2Label.setText( "Sim Pass");
                    m_Sim3Label.setText( "Sim Pocket");
                    m_Sim4Label.setText( "");
                    m_Sim1UpDown.setEnabled( true);
                    m_Sim2UpDown.setEnabled( true);
                    m_Sim3UpDown.setEnabled( true);
                    m_Sim4UpDown.setEnabled( false);
                    
                    m_Sim1UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_Sim2UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_Sim3UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_Sim4UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_PS_BC_PI_KABox.setEnabled( true);
                    m_PC_REC_QU_KABox.setEnabled( true);
                    m_ACCBox.setEnabled( true);
                    m_APBBox.setEnabled( true);
                    break;
            case SKILL:
                    m_A1Label.setText( "BC");
                    m_A2Label.setText( "REC");
                    m_A3Label.setText( "");
                    m_A4Label.setText( "");
                    m_Sim1Label.setText( "Sim Rush");
                    m_Sim2Label.setText( "Sim Catch");
                    m_Sim3Label.setText( "<HTML> Sim Punt<br>return</HTML>");
                    m_Sim4Label.setText( "<HTML> Sim Kick<br>return</HTML>");
                    m_Sim1UpDown.setEnabled( true);
                    m_Sim2UpDown.setEnabled( true);
                    m_Sim3UpDown.setEnabled( true);
                    m_Sim4UpDown.setEnabled( true);
                    m_Sim1UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_Sim2UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_Sim3UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_Sim4UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_PS_BC_PI_KABox.setEnabled( true);
                    m_PC_REC_QU_KABox.setEnabled( true);
                    m_ACCBox.setEnabled( false);
                    m_APBBox.setEnabled( false);
                    break;
            case OLINE:
                    m_A1Label.setText( "");
                    m_A2Label.setText( "");
                    m_A3Label.setText( "");
                    m_A4Label.setText( "");
                    m_Sim1Label.setText( "");
                    m_Sim2Label.setText( "");
                    m_Sim3Label.setText( "");
                    m_Sim4Label.setText( "");
                    m_Sim1UpDown.setEnabled( false);
                    m_Sim2UpDown.setEnabled( false);
                    m_Sim3UpDown.setEnabled( false);
                    m_Sim4UpDown.setEnabled( false);
                    m_PS_BC_PI_KABox.setEnabled( false);
                    m_PC_REC_QU_KABox.setEnabled( false);
                    m_ACCBox.setEnabled( false);
                    m_APBBox.setEnabled( false);
                    break;
            case DEFENSE:
                    m_A1Label.setText( "PI");
                    m_A2Label.setText( "QU");
                    m_A3Label.setText( "");
                    m_A4Label.setText( "");
                    m_Sim1Label.setText( "<HTML> Sim Pass<br>rush </HTML>");
                    m_Sim2Label.setText( "Sim Coverage");
                    m_Sim3Label.setText( "");
                    m_Sim4Label.setText( "");
                    m_Sim1UpDown.setEnabled( true);
                    m_Sim2UpDown.setEnabled( true);
                    m_Sim3UpDown.setEnabled( false);
                    m_Sim4UpDown.setEnabled( false);
                    m_Sim1UpDown.setModel(new SpinnerNumberModel(0, 0, 255, 1));
                    m_Sim2UpDown.setModel(new SpinnerNumberModel(0, 0, 255, 1));
                    m_Sim3UpDown.setModel(new SpinnerNumberModel(0, 0, 255, 1));
                    m_Sim4UpDown.setModel(new SpinnerNumberModel(0, 0, 255, 1));
                    m_PS_BC_PI_KABox.setEnabled( true);
                    m_PC_REC_QU_KABox.setEnabled( true);
                    m_ACCBox.setEnabled( false);
                    m_APBBox.setEnabled( false);
                    break;
            case KICKER:
                    m_A1Label.setText( "KA");
                    m_A2Label.setText( "AKB");
                    m_A3Label.setText( "");
                    m_A4Label.setText( "");
                    m_Sim1Label.setText( "Sim KA");
                    m_Sim2Label.setText( "");
                    m_Sim3Label.setText( "");
                    m_Sim4Label.setText( "");
                    m_Sim1UpDown.setEnabled( true);
                    m_Sim2UpDown.setEnabled( false);
                    m_Sim3UpDown.setEnabled( false);
                    m_Sim4UpDown.setEnabled( false);
                    m_Sim1UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_Sim2UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_Sim3UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_Sim4UpDown.setModel(new SpinnerNumberModel(0, 0, 15, 1));
                    m_PS_BC_PI_KABox.setEnabled( true);
                    m_PC_REC_QU_KABox.setEnabled( true);
                    m_ACCBox.setEnabled( false);
                    m_APBBox.setEnabled( false);
                    break;
        }
        m_DoneInit = true;
    }
    
    private void UpdatePosition() throws IOException
    {
        if( m_DoneInit )
        {
            TSBPlayer pos = TSBPlayer.valueOf(mPositionComboBox.getSelectedItem().toString());
            switch(pos)
            {
                case QB1: case QB2:
                        setCurrentState(StateEnum.QB);
                        break;
                case RB1: case RB2: 
                case RB3: case RB4:
                case WR1: case WR2: 
                case WR3: case WR4:
                case TE1: case TE2:
                        setCurrentState(StateEnum.SKILL);
                        break;
                case RT: case RG: 
                case C: case LG: 
                case LT:
                        setCurrentState(StateEnum.OLINE);
                        break;
                case RE:   case LE:   
                case NT:
                case ROLB: case RILB: 
                case LILB: case LOLB:
                case RCB:  case LCB:  
                case SS:   case FS:
                        setCurrentState(StateEnum.DEFENSE);
                        break;
                case P: case K:
                        setCurrentState(StateEnum.KICKER);
                        break;
            }
            SetCurrentPlayer();
        }
    }
    
    
    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents()
    {

        jComboBox1 = new javax.swing.JComboBox();
        jLabel1 = new javax.swing.JLabel();
        mTeamsComboBox = new javax.swing.JComboBox();
        mPositionComboBox = new javax.swing.JComboBox();
        positionLabel = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        mJerseyNumberUpDown = new javax.swing.JSpinner();
        mFirstNameTextBox = new javax.swing.JTextField();
        jLabel3 = new javax.swing.JLabel();
        mLastNameTextBox = new javax.swing.JTextField();
        jLabel4 = new javax.swing.JLabel();
        mAttributeGroupBox = new javax.swing.JPanel();
        jLabel5 = new javax.swing.JLabel();
        m_RSBox = new javax.swing.JComboBox();
        m_RPBox = new javax.swing.JComboBox();
        jLabel6 = new javax.swing.JLabel();
        m_MSBox = new javax.swing.JComboBox();
        jLabel7 = new javax.swing.JLabel();
        m_HPBox = new javax.swing.JComboBox();
        jLabel8 = new javax.swing.JLabel();
        m_PS_BC_PI_KABox = new javax.swing.JComboBox();
        m_A1Label = new javax.swing.JLabel();
        m_PC_REC_QU_KABox = new javax.swing.JComboBox();
        m_A2Label = new javax.swing.JLabel();
        m_ACCBox = new javax.swing.JComboBox();
        m_A3Label = new javax.swing.JLabel();
        m_APBBox = new javax.swing.JComboBox();
        m_A4Label = new javax.swing.JLabel();
        jPanel1 = new javax.swing.JPanel();
        m_Sim1UpDown = new javax.swing.JSpinner();
        m_Sim1Label = new javax.swing.JLabel();
        m_Sim2UpDown = new javax.swing.JSpinner();
        m_Sim2Label = new javax.swing.JLabel();
        m_Sim3Label = new javax.swing.JLabel();
        m_Sim3UpDown = new javax.swing.JSpinner();
        m_Sim4Label = new javax.swing.JLabel();
        m_Sim4UpDown = new javax.swing.JSpinner();
        mFaceBox = new javax.swing.JLabel();
        m_FaceLabel = new javax.swing.JLabel();
        mPrevPicture = new javax.swing.JButton();
        mNextPicture = new javax.swing.JButton();
        mPrevPlayerButton = new javax.swing.JButton();
        mNextPlayerButton = new javax.swing.JButton();
        mCancelButton = new javax.swing.JButton();
        mOKButton = new javax.swing.JButton();

        jComboBox1.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "Item 1", "Item 2", "Item 3", "Item 4" }));

        setTitle("Modify Players");
        setIconImage( MainGUI.GetImage("/tsbtool_gui/facepackage/00.png", this));
        setMaximumSize(new java.awt.Dimension(531, 371));
        setMinimumSize(new java.awt.Dimension(530, 370));
        setName("AttributeForm"); // NOI18N
        setPreferredSize(new java.awt.Dimension(416, 312));
        getContentPane().setLayout(null);

        jLabel1.setText("Team");
        getContentPane().add(jLabel1);
        jLabel1.setBounds(10, 14, 32, 16);

        mTeamsComboBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "Item 1", "Item 2", "Item 3", "Item 4" }));
        getContentPane().add(mTeamsComboBox);
        mTeamsComboBox.setBounds(46, 11, 98, 26);

        mPositionComboBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "QB1", "QB2", "RB1", "RB2", "RB3", "RB4", "WR1", "WR2", "WR3", "WR4", "TE1", "TE2", "C", "LG", "RG", "LT", "RT", "RE", "NT", "LE", "ROLB", "RILB", "LILB", "LOLB", "RCB", "LCB", "FS", "SS", "K", "P" }));
        mPositionComboBox.addActionListener(new java.awt.event.ActionListener()
        {
            public void actionPerformed(java.awt.event.ActionEvent evt)
            {
                mPositionComboBoxActionPerformed(evt);
            }
        });
        getContentPane().add(mPositionComboBox);
        mPositionComboBox.setBounds(310, 11, 98, 26);

        positionLabel.setText("Position");
        getContentPane().add(positionLabel);
        positionLabel.setBounds(263, 14, 45, 16);

        jLabel2.setText("#");
        getContentPane().add(jLabel2);
        jLabel2.setBounds(10, 42, 7, 16);
        getContentPane().add(mJerseyNumberUpDown);
        mJerseyNumberUpDown.setBounds(10, 62, 50, 28);
        getContentPane().add(mFirstNameTextBox);
        mFirstNameTextBox.setBounds(70, 62, 142, 28);

        jLabel3.setText("First Name");
        getContentPane().add(jLabel3);
        jLabel3.setBounds(70, 42, 63, 16);
        getContentPane().add(mLastNameTextBox);
        mLastNameTextBox.setBounds(263, 62, 142, 28);

        jLabel4.setText("Last Name");
        getContentPane().add(jLabel4);
        jLabel4.setBounds(263, 42, 63, 16);

        mAttributeGroupBox.setBorder(javax.swing.BorderFactory.createTitledBorder("Attributes"));
        mAttributeGroupBox.setLayout(null);

        jLabel5.setText("RS");
        mAttributeGroupBox.add(jLabel5);
        jLabel5.setBounds(20, 20, 17, 16);

        m_RSBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "6", "13", "19", "25", "31", "38", "44", "50", "56", "63", "69", "75", "81", "88", "94", "100" }));
        m_RSBox.setMaximumSize(new java.awt.Dimension(56, 20));
        mAttributeGroupBox.add(m_RSBox);
        m_RSBox.setBounds(10, 40, 60, 26);

        m_RPBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "6", "13", "19", "25", "31", "38", "44", "50", "56", "63", "69", "75", "81", "88", "94", "100" }));
        m_RPBox.setMaximumSize(new java.awt.Dimension(43, 20));
        mAttributeGroupBox.add(m_RPBox);
        m_RPBox.setBounds(70, 40, 60, 26);

        jLabel6.setText("RP");
        mAttributeGroupBox.add(jLabel6);
        jLabel6.setBounds(80, 20, 17, 16);

        m_MSBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "6", "13", "19", "25", "31", "38", "44", "50", "56", "63", "69", "75", "81", "88", "94", "100" }));
        m_MSBox.setMaximumSize(new java.awt.Dimension(43, 20));
        mAttributeGroupBox.add(m_MSBox);
        m_MSBox.setBounds(130, 40, 60, 26);

        jLabel7.setText("MS");
        mAttributeGroupBox.add(jLabel7);
        jLabel7.setBounds(140, 20, 17, 16);

        m_HPBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "6", "13", "19", "25", "31", "38", "44", "50", "56", "63", "69", "75", "81", "88", "94", "100" }));
        m_HPBox.setMaximumSize(new java.awt.Dimension(43, 20));
        mAttributeGroupBox.add(m_HPBox);
        m_HPBox.setBounds(190, 40, 60, 26);

        jLabel8.setText("HP");
        mAttributeGroupBox.add(jLabel8);
        jLabel8.setBounds(200, 20, 17, 16);

        m_PS_BC_PI_KABox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "6", "13", "19", "25", "31", "38", "44", "50", "56", "63", "69", "75", "81", "88", "94", "100" }));
        m_PS_BC_PI_KABox.setMaximumSize(new java.awt.Dimension(43, 20));
        mAttributeGroupBox.add(m_PS_BC_PI_KABox);
        m_PS_BC_PI_KABox.setBounds(250, 40, 60, 26);

        m_A1Label.setText("PS");
        mAttributeGroupBox.add(m_A1Label);
        m_A1Label.setBounds(260, 20, 16, 16);

        m_PC_REC_QU_KABox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "6", "13", "19", "25", "31", "38", "44", "50", "56", "63", "69", "75", "81", "88", "94", "100" }));
        m_PC_REC_QU_KABox.setMaximumSize(new java.awt.Dimension(43, 20));
        mAttributeGroupBox.add(m_PC_REC_QU_KABox);
        m_PC_REC_QU_KABox.setBounds(310, 40, 60, 26);

        m_A2Label.setText("PC");
        mAttributeGroupBox.add(m_A2Label);
        m_A2Label.setBounds(310, 20, 17, 16);

        m_ACCBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "6", "13", "19", "25", "31", "38", "44", "50", "56", "63", "69", "75", "81", "88", "94", "100" }));
        m_ACCBox.setMaximumSize(new java.awt.Dimension(43, 20));
        mAttributeGroupBox.add(m_ACCBox);
        m_ACCBox.setBounds(370, 40, 60, 26);

        m_A3Label.setText("ACC");
        mAttributeGroupBox.add(m_A3Label);
        m_A3Label.setBounds(380, 20, 25, 16);

        m_APBBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "6", "13", "19", "25", "31", "38", "44", "50", "56", "63", "69", "75", "81", "88", "94", "100" }));
        m_APBBox.setMaximumSize(new java.awt.Dimension(43, 20));
        mAttributeGroupBox.add(m_APBBox);
        m_APBBox.setBounds(430, 40, 60, 26);

        m_A4Label.setText("APB");
        mAttributeGroupBox.add(m_A4Label);
        m_A4Label.setBounds(440, 20, 23, 16);

        getContentPane().add(mAttributeGroupBox);
        mAttributeGroupBox.setBounds(10, 88, 500, 80);

        jPanel1.setBorder(javax.swing.BorderFactory.createTitledBorder("Sim attributes"));
        jPanel1.setLayout(null);
        jPanel1.add(m_Sim1UpDown);
        m_Sim1UpDown.setBounds(20, 60, 60, 28);

        m_Sim1Label.setText("Sim Rush");
        m_Sim1Label.setVerticalAlignment(javax.swing.SwingConstants.TOP);
        jPanel1.add(m_Sim1Label);
        m_Sim1Label.setBounds(20, 20, 70, 40);
        jPanel1.add(m_Sim2UpDown);
        m_Sim2UpDown.setBounds(100, 60, 60, 28);

        m_Sim2Label.setText("Sim Pass");
        m_Sim2Label.setVerticalAlignment(javax.swing.SwingConstants.TOP);
        jPanel1.add(m_Sim2Label);
        m_Sim2Label.setBounds(100, 20, 80, 40);

        m_Sim3Label.setText("Sim Pocket");
        m_Sim3Label.setVerticalAlignment(javax.swing.SwingConstants.TOP);
        jPanel1.add(m_Sim3Label);
        m_Sim3Label.setBounds(180, 20, 70, 40);
        jPanel1.add(m_Sim3UpDown);
        m_Sim3UpDown.setBounds(180, 60, 60, 28);

        m_Sim4Label.setText("Sim Kick Ret");
        m_Sim4Label.setVerticalAlignment(javax.swing.SwingConstants.TOP);
        jPanel1.add(m_Sim4Label);
        m_Sim4Label.setBounds(260, 20, 70, 40);
        jPanel1.add(m_Sim4UpDown);
        m_Sim4UpDown.setBounds(260, 60, 60, 28);

        getContentPane().add(jPanel1);
        jPanel1.setBounds(150, 170, 350, 110);

        mFaceBox.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/facepackage/00.png"))); // NOI18N
        mFaceBox.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                mFaceBoxMouseClicked(evt);
            }
        });
        getContentPane().add(mFaceBox);
        mFaceBox.setBounds(10, 180, 65, 64);

        m_FaceLabel.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        m_FaceLabel.setText("C0");
        getContentPane().add(m_FaceLabel);
        m_FaceLabel.setBounds(10, 250, 63, 16);

        mPrevPicture.setText("\\/");
        mPrevPicture.addActionListener(new java.awt.event.ActionListener()
        {
            public void actionPerformed(java.awt.event.ActionEvent evt)
            {
                mPrevPictureActionPerformed(evt);
            }
        });
        getContentPane().add(mPrevPicture);
        mPrevPicture.setBounds(10, 270, 50, 28);

        mNextPicture.setText("/\\");
            mNextPicture.addActionListener(new java.awt.event.ActionListener()
            {
                public void actionPerformed(java.awt.event.ActionEvent evt)
                {
                    mNextPictureActionPerformed(evt);
                }
            });
            getContentPane().add(mNextPicture);
            mNextPicture.setBounds(60, 270, 50, 28);

            mPrevPlayerButton.setText("Prev Player");
            mPrevPlayerButton.addActionListener(new java.awt.event.ActionListener()
            {
                public void actionPerformed(java.awt.event.ActionEvent evt)
                {
                    mPrevPlayerButtonActionPerformed(evt);
                }
            });
            getContentPane().add(mPrevPlayerButton);
            mPrevPlayerButton.setBounds(10, 300, 89, 28);

            mNextPlayerButton.setText("Next Player");
            mNextPlayerButton.addActionListener(new java.awt.event.ActionListener()
            {
                public void actionPerformed(java.awt.event.ActionEvent evt)
                {
                    mNextPlayerButtonActionPerformed(evt);
                }
            });
            getContentPane().add(mNextPlayerButton);
            mNextPlayerButton.setBounds(100, 300, 89, 28);

            mCancelButton.setText("Cancel");
            mCancelButton.addActionListener(new java.awt.event.ActionListener()
            {
                public void actionPerformed(java.awt.event.ActionEvent evt)
                {
                    mCancelButtonActionPerformed(evt);
                }
            });
            getContentPane().add(mCancelButton);
            mCancelButton.setBounds(400, 300, 82, 28);

            mOKButton.setText("OK");
            mOKButton.addActionListener(new java.awt.event.ActionListener()
            {
                public void actionPerformed(java.awt.event.ActionEvent evt)
                {
                    mOKButtonActionPerformed(evt);
                }
            });
            getContentPane().add(mOKButton);
            mOKButton.setBounds(320, 300, 70, 28);

            pack();
        }// </editor-fold>//GEN-END:initComponents

    private void mCancelButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mCancelButtonActionPerformed
        mCanceled = true;
        setVisible(false);
    }//GEN-LAST:event_mCancelButtonActionPerformed

    private void mOKButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mOKButtonActionPerformed
        mCanceled = false;
        ReplacePlayer();
        setVisible(false);
    }//GEN-LAST:event_mOKButtonActionPerformed

    private void mPositionComboBoxActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mPositionComboBoxActionPerformed
        try{ UpdatePosition(); }
        catch(Exception e){
            System.err.println("Error! "+ e.getMessage());
        }
    }//GEN-LAST:event_mPositionComboBoxActionPerformed

    private void mNextPlayerButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mNextPlayerButtonActionPerformed
        
        if( mPositionComboBox.getSelectedIndex() == mPositionComboBox.getModel().getSize() - 1 )
        {
            mPositionComboBox.setSelectedIndex(0);
            if( mTeamsComboBox.getSelectedIndex() == mTeamsComboBox.getModel().getSize() - 1 )
                mTeamsComboBox.setSelectedIndex(0);
            else
                mTeamsComboBox.setSelectedIndex(mTeamsComboBox.getSelectedIndex()+1);
        }
        else
            mPositionComboBox.setSelectedIndex(mPositionComboBox.getSelectedIndex()+1);
    }//GEN-LAST:event_mNextPlayerButtonActionPerformed

    private void mPrevPlayerButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mPrevPlayerButtonActionPerformed
        if( mPositionComboBox.getSelectedIndex() == 0 )
        {
            mPositionComboBox.setSelectedIndex(mPositionComboBox.getModel().getSize() - 1);
            if( mTeamsComboBox.getSelectedIndex() == 0 )
                mTeamsComboBox.setSelectedIndex(mTeamsComboBox.getModel().getSize() - 1);
            else
                mTeamsComboBox.setSelectedIndex(mTeamsComboBox.getSelectedIndex()-1);
        }
        else
            mPositionComboBox.setSelectedIndex(mPositionComboBox.getSelectedIndex()-1);
    }//GEN-LAST:event_mPrevPlayerButtonActionPerformed

    private void mPrevPictureActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mPrevPictureActionPerformed
        if( m_ImageNumber == 0x00 )
            m_ImageNumber = 0xD4;
        else if( m_ImageNumber == 0x80 )
            m_ImageNumber = 0x52;
        else
            m_ImageNumber--;
        ShowCurrentFace();
    }//GEN-LAST:event_mPrevPictureActionPerformed

    private void mNextPictureActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mNextPictureActionPerformed
        if( m_ImageNumber == 0xD4 )
                m_ImageNumber = 0;
        else if( m_ImageNumber == 0x52 )
                m_ImageNumber = 0x80;
        else
                m_ImageNumber++;
        ShowCurrentFace();

    }//GEN-LAST:event_mNextPictureActionPerformed

    private void mFaceBoxMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_mFaceBoxMouseClicked
        FaceForm form = new FaceForm(this, true);
        
        form.setVisible(true);
        int userChoice = form.getImageIndex();
        
        if(userChoice > -1)
        {
            m_ImageNumber = userChoice;
            ShowCurrentFace();
        }
        form.dispose();
        
    }//GEN-LAST:event_mFaceBoxMouseClicked

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
            java.util.logging.Logger.getLogger(AttributeForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(AttributeForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(AttributeForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(AttributeForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new AttributeForm().setVisible(true);
            }
        });
    }
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JComboBox jComboBox1;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel7;
    private javax.swing.JLabel jLabel8;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel mAttributeGroupBox;
    private javax.swing.JButton mCancelButton;
    private javax.swing.JLabel mFaceBox;
    private javax.swing.JTextField mFirstNameTextBox;
    private javax.swing.JSpinner mJerseyNumberUpDown;
    private javax.swing.JTextField mLastNameTextBox;
    private javax.swing.JButton mNextPicture;
    private javax.swing.JButton mNextPlayerButton;
    private javax.swing.JButton mOKButton;
    private javax.swing.JComboBox mPositionComboBox;
    private javax.swing.JButton mPrevPicture;
    private javax.swing.JButton mPrevPlayerButton;
    private javax.swing.JComboBox mTeamsComboBox;
    private javax.swing.JLabel m_A1Label;
    private javax.swing.JLabel m_A2Label;
    private javax.swing.JLabel m_A3Label;
    private javax.swing.JLabel m_A4Label;
    private javax.swing.JComboBox m_ACCBox;
    private javax.swing.JComboBox m_APBBox;
    private javax.swing.JLabel m_FaceLabel;
    private javax.swing.JComboBox m_HPBox;
    private javax.swing.JComboBox m_MSBox;
    private javax.swing.JComboBox m_PC_REC_QU_KABox;
    private javax.swing.JComboBox m_PS_BC_PI_KABox;
    private javax.swing.JComboBox m_RPBox;
    private javax.swing.JComboBox m_RSBox;
    private javax.swing.JLabel m_Sim1Label;
    private javax.swing.JSpinner m_Sim1UpDown;
    private javax.swing.JLabel m_Sim2Label;
    private javax.swing.JSpinner m_Sim2UpDown;
    private javax.swing.JLabel m_Sim3Label;
    private javax.swing.JSpinner m_Sim3UpDown;
    private javax.swing.JLabel m_Sim4Label;
    private javax.swing.JSpinner m_Sim4UpDown;
    private javax.swing.JLabel positionLabel;
    // End of variables declaration//GEN-END:variables
}
