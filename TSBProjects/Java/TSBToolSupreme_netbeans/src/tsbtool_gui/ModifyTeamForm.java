/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package tsbtool_gui;

import java.awt.Image;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import javax.swing.ComboBoxModel;
import javax.swing.DefaultComboBoxModel;
import javax.swing.ImageIcon;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JSpinner;
import javax.swing.SpinnerNumberModel;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;
import tsbtoolsupreme.*;

/**
 *
 * @author Chris
 */
public class ModifyTeamForm extends javax.swing.JDialog {

    private boolean m_InitDone = false;
    private InputParser m_InputParser = null; 
    private ImageIcon[][] m_PlayImages;
    private JLabel[] m_Boxes;
    private JSpinner[] m_Spinners;
    private Pattern m_PlaybookRegex;
    private Pattern m_OffensiveFormationRegex;
    /**
     * Creates new form ModifyTeamForm
     */
    public ModifyTeamForm(java.awt.Frame parent, boolean modal) {
        super(parent, modal);
        m_InitDone = false;
        initComponents();

        m_PlaybookRegex    = Pattern.compile("PLAYBOOK (R[1-8]{4})\\s*,\\s*(P[1-8]{4})");
        m_OffensiveFormationRegex = Pattern.compile("OFFENSIVE_FORMATION\\s*=\\s*([a-zA-Z1234_]+)");
        m_Boxes = new JLabel[8];
        m_Boxes[0] = m_Run1PictureBox;
        m_Boxes[1] = m_Run2PictureBox;
        m_Boxes[2] = m_Run3PictureBox;
        m_Boxes[3] = m_Run4PictureBox;
        m_Boxes[4] = m_Pass1PictureBox;
        m_Boxes[5] = m_Pass2PictureBox;
        m_Boxes[6] = m_Pass3PictureBox;
        m_Boxes[7] = m_Pass4PictureBox;
        
        m_RunPlaysGroupBox.setEnabled(false);
        m_PassPlaysGroupBox.setEnabled(false);
        m_FormationComboBox.setEnabled(false);
        m_InputParser = new InputParser();
        PopulatePlayImages();
        m_TeamsComboBox.setSelectedIndex( 0);
        
        m_Spinners = new JSpinner[8];
        m_Spinners[0] = m_Run1UpDown;
        m_Spinners[1] = m_Run2UpDown;
        m_Spinners[2] = m_Run3UpDown;
        m_Spinners[3] = m_Run4UpDown;
        m_Spinners[4] = m_Pass1UpDown;
        m_Spinners[5] = m_Pass2UpDown;
        m_Spinners[6] = m_Pass3UpDown;
        m_Spinners[7] = m_Pass4UpDown;
        ChangeListener listener = new ChangeListener() {
            public void stateChanged(ChangeEvent e) {
                m_UpDown_ValueChanged((JSpinner) e.getSource() );
           }
        };
    
         m_Pass1UpDown.addChangeListener(listener);
         m_Pass2UpDown.addChangeListener(listener);
         m_Pass3UpDown.addChangeListener(listener);
         m_Pass4UpDown.addChangeListener(listener);
         m_Run1UpDown.addChangeListener(listener);
         m_Run2UpDown.addChangeListener(listener);
         m_Run3UpDown.addChangeListener(listener);
         m_Run4UpDown.addChangeListener(listener);
         
         ChangeListener listener2 = new ChangeListener() {
            public void stateChanged(ChangeEvent e) {
                UpdateData();
           }
        };
        m_SimDefenseUpDown.addChangeListener(listener2);
        m_SimOffenseUpDown.addChangeListener(listener2);
        m_InitDone = true;
    }

    private boolean mCanceled = false;
    
    public boolean getCanceled(){
        return mCanceled;
    }
    
    private void m_UpDown_ValueChanged(JSpinner ud)
    {
        if( ud != null )
        {
            int i = 0; //ud.TabIndex;
            for(int index = 0; index< m_Boxes.length; index++)
            {
                if(m_Spinners[index] == ud )
                {
                    i= index;
                    break;
                }
            }
            int j = (int)ud.getValue();
            UpdatePictureBox(i, j-1);
        }
            UpdateData();
    }
    
    private String m_Data = "";
    /// <summary>
    /// The data to work on.
    /// </summary>
    public String getData()
    {
        return m_Data; 
    }

    public void setData(String value)
    { 
        m_InitDone = false;
        m_Data = value;
        if( m_Data.indexOf("TEAM") != -1 )
        {
            if( m_Data.indexOf("PLAYBOOK") > -1 )
            {
                m_RunPlaysGroupBox.setEnabled(true);
                m_PassPlaysGroupBox.setEnabled(true);
            }

            if( m_Data.indexOf("OFFENSIVE_FORMATION") > -1 )
            {
                m_FormationComboBox.setEnabled(true);
            }
            SetupTeams();
            ShowTeamValues();
        }
        m_InitDone = true;
    }

    public String getCurrentTeam()
    {
        return m_TeamsComboBox.getSelectedItem().toString();
    }

    public void setCurrentTeam(String value)
    {
        //int index = m_TeamsComboBox.Items.indexOf(value);
        int index = indexOfValue(m_TeamsComboBox, value);
        if(index > -1 )
        {
            m_TeamsComboBox.setSelectedIndex( index);
        }
    }

    private void SetupTeams()
    {
        Pattern teamRegex = Pattern.compile("TEAM\\s*=\\s*([a-z0-9]+)");
        Matcher mc = teamRegex.matcher(m_Data);

        DefaultComboBoxModel model = new DefaultComboBoxModel();
        String team;
        while(mc.find())
        {
            team = mc.group(1);
            model.addElement(team );
        }
        m_TeamsComboBox.setModel(model);
        if( model.getSize() > 0 )
        {
            m_TeamsComboBox.setSelectedIndex(0);
        }
    }

    /// <summary>
    /// Reads in the play images into the m_PlayImages array.
    /// </summary>
    private void PopulatePlayImages()
    {
        m_PlayImages = new ImageIcon[8][];
        for(int i = 0; i < m_PlayImages.length; i++)
        {
            m_PlayImages[i] = new ImageIcon[8];
        }

        String fileName ="";
        String type = "R";
        int num = 0;
        for(int i = 0; i < m_PlayImages.length; i++)
        {
            if( i > 3 )
            {
                type = "P";
                num = i-4;
            }
            else 
                num = i;
            for(int j = 0; j < m_PlayImages[i].length; j++)
            {// m_Run1PictureBox.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/plays/P1-0.png")));
                fileName = String.format("/tsbtool_gui/plays/%s%d-%d.BMP",
                    type,num+1,j);
                m_PlayImages[i][j] = new ImageIcon( MainGUI.GetImage(fileName, this));
            }
        }
    }

    /// <summary>
    /// Returns the currently selected offensive formation String.
    /// </summary>
    /// <returns></returns>
    private String GetOffensiveFormation()
    {
        String ret = String.format("OFFENSIVE_FORMATION = %s",
            m_FormationComboBox.getSelectedItem().toString());
        return ret;
    }

    /// <summary>
    /// Gets the current playbook String.
    /// </summary>
    /// <returns></returns>
    private String GetCurrentPlaybook()
    {
        String ret = String.format("PLAYBOOK R%d%d%d%d, P%d%d%d%d ",
            m_Run1UpDown.getValue(),
            m_Run2UpDown.getValue(),
            m_Run3UpDown.getValue(),
            m_Run4UpDown.getValue(),
            m_Pass1UpDown.getValue(),
            m_Pass2UpDown.getValue(),
            m_Pass3UpDown.getValue(),
            m_Pass4UpDown.getValue()
            );
        return ret;
    }

    /// <summary>
    /// Shows the data for the current team.
    /// </summary>
    private void ShowTeamValues()
    {
        String team    = m_TeamsComboBox.getSelectedItem().toString();
        String line = GetTeamString(team);

        if( line != null )
        {
            int[] vals = m_InputParser.GetSimData(line);

            if( vals != null && vals[1] > -1 && vals[1] < 4 )
                m_OffensivePrefomComboBox.setSelectedIndex( vals[1]);

            int[] simVals = GetNibbles(vals[0]);
            m_SimOffenseUpDown.setValue( simVals[0]);
            m_SimDefenseUpDown.setValue( simVals[1]);

            Matcher ofMatch = m_OffensiveFormationRegex.matcher(line);
            Matcher pbMatch = m_PlaybookRegex.matcher(line);
            if( ofMatch.find() )
            {
                String val = ofMatch.group(1).toString();
                int index = indexOfValue(m_FormationComboBox,val);
                if( index > -1 )
                    m_FormationComboBox.setSelectedIndex( index);
            }
            if( pbMatch.find() )
            {
                String runs   = pbMatch.group(1);
                String passes = pbMatch.group(2);
                SetRuns(runs);
                SetPasses(passes);
            }
        }
    }
    
    private int indexOfValue(JComboBox box, Object value)
    {
        int index = -1;
        ComboBoxModel model = box.getModel();
        for(int i = 0; i < model.getSize(); i++)
        {
            if( value.equals(model.getElementAt(i)))
            {
                index = i;
                break;
            }
        }
        return index;
    }

    private void SetRuns(String runs)
    {
        if(runs != null && runs.length() == 5)
        {
            m_Run1UpDown.setValue( Integer.parseInt(""+ runs.charAt(1)));
            m_Run2UpDown.setValue( Integer.parseInt(""+ runs.charAt(2)));
            m_Run3UpDown.setValue( Integer.parseInt(""+ runs.charAt(3)));
            m_Run4UpDown.setValue( Integer.parseInt(""+ runs.charAt(4)));
        }
    }

    private void SetPasses(String passes)
    {
        if(passes != null && passes.length() == 5)
        {
            m_Pass1UpDown.setValue( Integer.parseInt(""+ passes.charAt(1)));
            m_Pass2UpDown.setValue( Integer.parseInt(""+ passes.charAt(2)));
            m_Pass3UpDown.setValue( Integer.parseInt(""+ passes.charAt(3)));
            m_Pass4UpDown.setValue( Integer.parseInt(""+ passes.charAt(4)));
        }
    }

    /// <summary>
    /// Gets a String like:
    /// "TEAM = bills SimData=0xab0"
    /// that is currently from m_Data.
    /// </summary>
    /// <param name="team"></param>
    /// <returns></returns>
    private String GetTeamString(String team)
    {
        String theTeam = String.format("TEAM = %s",team);
        int teamIndex  = m_Data.indexOf(theTeam);
        int newLine    = -1;
        String line = null;

        if( teamIndex > -1 && (newLine = m_Data.indexOf('\n',teamIndex)) > -1 )
        {
            line = m_Data.substring(teamIndex, newLine );
        }
        if( line != null && m_PassPlaysGroupBox.isEnabled() )
        {
            Matcher m = m_PlaybookRegex.matcher(m_Data);
            m.find(newLine);
            line = line + "\n" + m.group(); //TODO: verify this works
        }
        return line;
    }

    /// <summary>
    /// Gwts a String that represents the values set in the UI.
    /// Like:
    /// "TEAM = bills SimData=0xab0"
    /// </summary>
    /// <returns></returns>
    private String GetCurrentValues()
    {
        Integer simO = (Integer)m_SimOffenseUpDown.getValue();
        Integer simD = (Integer)m_SimDefenseUpDown.getValue();
//        int simO_i = 0xff & simO;
//        int simD_i = 0xff & simD;
        String ret = String.format("%x%x%d",
            simO,
            simD,
            m_OffensivePrefomComboBox.getSelectedIndex());

        return ret;
    }

    /// <summary>
    /// Gets the text representation of the current UI.
    /// </summary>
    /// <returns></returns>
    private String GetCurrentTeamString()
    {
        String vals = GetCurrentValues();
        String ret = String.format("TEAM = %s SimData=0x%s",
            m_TeamsComboBox.getSelectedItem(),
            vals);
        if( m_FormationComboBox.isEnabled() )
            ret = ret + ", "+GetOffensiveFormation();
        if(m_RunPlaysGroupBox.isEnabled()  )
            ret = ret + "\n"+ GetCurrentPlaybook();

        return ret;
    }

    private void UpdateData()
    {
        if(m_InitDone)
        {
            String team     = m_TeamsComboBox.getSelectedItem().toString();
            String oldValue = GetTeamString(team);
            String newValue = GetCurrentTeamString();

            m_Data = m_Data.replace(oldValue, newValue);
        }
    }

    /// <summary>
    /// Returns the associated nibbles for the value passed (assuming it's a byte).
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    private int[] GetNibbles(int val)
    {
        int[] ret = new int[2];
        int byteValue = val;
        ret[1] = (byteValue & 0x0F); // lo byte
        ret[0] = (byteValue >> 4);
        return ret;
    }

    private void UpdatePictureBox(int pictureBoxNumber, int pictureNumber)
    {
        ImageIcon img = m_PlayImages[pictureBoxNumber][pictureNumber];
        m_Boxes[pictureBoxNumber].setIcon(img);
    }
    
    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jLabel1 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jLabel3 = new javax.swing.JLabel();
        jLabel4 = new javax.swing.JLabel();
        m_TeamsComboBox = new javax.swing.JComboBox();
        m_SimOffenseUpDown = new javax.swing.JSpinner();
        m_SimDefenseUpDown = new javax.swing.JSpinner();
        m_OffensivePrefomComboBox = new javax.swing.JComboBox();
        jLabel5 = new javax.swing.JLabel();
        m_FormationComboBox = new javax.swing.JComboBox();
        m_RunPlaysGroupBox = new javax.swing.JPanel();
        m_Run1PictureBox = new javax.swing.JLabel();
        m_Run1UpDown = new javax.swing.JSpinner();
        m_Run2UpDown = new javax.swing.JSpinner();
        m_Run2PictureBox = new javax.swing.JLabel();
        m_Run3UpDown = new javax.swing.JSpinner();
        m_Run3PictureBox = new javax.swing.JLabel();
        m_Run4UpDown = new javax.swing.JSpinner();
        m_Run4PictureBox = new javax.swing.JLabel();
        m_PassPlaysGroupBox = new javax.swing.JPanel();
        m_Pass1PictureBox = new javax.swing.JLabel();
        m_Pass1UpDown = new javax.swing.JSpinner();
        m_Pass2UpDown = new javax.swing.JSpinner();
        m_Pass2PictureBox = new javax.swing.JLabel();
        m_Pass3UpDown = new javax.swing.JSpinner();
        m_Pass3PictureBox = new javax.swing.JLabel();
        m_Pass4UpDown = new javax.swing.JSpinner();
        m_Pass4PictureBox = new javax.swing.JLabel();
        m_SaveTeamButton = new javax.swing.JButton();
        m_OKButton = new javax.swing.JButton();
        m_CancelButton = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setMaximumSize(new java.awt.Dimension(500, 500));
        setMinimumSize(new java.awt.Dimension(424, 456));
        setPreferredSize(new java.awt.Dimension(490, 510));

        jLabel1.setText("Team");

        jLabel2.setText("<HTML>Sim<br>Offense</HTML>");

        jLabel3.setText("Run/Pass Ratio");

        jLabel4.setText("<HTML>Sim<br>Defense</HTML>");

        m_TeamsComboBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "bills", "colts", "dolphins", "patriots", "jets", "bengals", "browns", "oilers", "steelers", "broncos", "chiefs", "raiders", "chargers", "seahawks", "redskins", "giants", "eagles", "cardinals", "cowboys", "bears", "lions", "packers", "vikings", "buccaneers", "49ers", "rams", "saints", "falcons" }));
        m_TeamsComboBox.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                m_TeamsComboBoxActionPerformed(evt);
            }
        });

        m_SimOffenseUpDown.setModel(new javax.swing.SpinnerNumberModel(0, 0, 15, 1));

        m_SimDefenseUpDown.setModel(new javax.swing.SpinnerNumberModel(0, 0, 15, 1));

        m_OffensivePrefomComboBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "Balance Rush", "Heavy Rushing", "Balance Pass", "Heavy Pass" }));
        m_OffensivePrefomComboBox.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                m_OffensivePrefomComboBoxActionPerformed(evt);
            }
        });

        jLabel5.setText("Formation");

        m_FormationComboBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "2RB_2WR_1TE", "1RB_4WR", "1RB_3WR_1TE" }));
        m_FormationComboBox.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                m_FormationComboBoxActionPerformed(evt);
            }
        });

        m_RunPlaysGroupBox.setBorder(javax.swing.BorderFactory.createTitledBorder("Run Plays"));

        m_Run1PictureBox.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/plays/P1-0.png"))); // NOI18N
        m_Run1PictureBox.setName(""); // NOI18N

        m_Run1UpDown.setModel(new javax.swing.SpinnerNumberModel(1, 1, 8, 1));

        m_Run2UpDown.setModel(new javax.swing.SpinnerNumberModel(1, 1, 8, 1));

        m_Run2PictureBox.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/plays/P1-0.png"))); // NOI18N

        m_Run3UpDown.setModel(new javax.swing.SpinnerNumberModel(1, 1, 8, 1));

        m_Run3PictureBox.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/plays/P1-0.png"))); // NOI18N

        m_Run4UpDown.setModel(new javax.swing.SpinnerNumberModel(1, 1, 8, 1));

        m_Run4PictureBox.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/plays/P1-0.png"))); // NOI18N

        javax.swing.GroupLayout m_RunPlaysGroupBoxLayout = new javax.swing.GroupLayout(m_RunPlaysGroupBox);
        m_RunPlaysGroupBox.setLayout(m_RunPlaysGroupBoxLayout);
        m_RunPlaysGroupBoxLayout.setHorizontalGroup(
            m_RunPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(m_RunPlaysGroupBoxLayout.createSequentialGroup()
                .addGroup(m_RunPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(m_Run1PictureBox, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(m_Run1UpDown))
                .addGap(31, 31, 31)
                .addGroup(m_RunPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(m_Run2PictureBox, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(m_Run2UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(31, 31, 31)
                .addGroup(m_RunPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(m_Run3PictureBox, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(m_Run3UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(m_RunPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(m_Run4PictureBox, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(m_Run4UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(14, 14, 14))
        );
        m_RunPlaysGroupBoxLayout.setVerticalGroup(
            m_RunPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(m_RunPlaysGroupBoxLayout.createSequentialGroup()
                .addComponent(m_Run1PictureBox, javax.swing.GroupLayout.PREFERRED_SIZE, 74, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(m_Run1UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
            .addGroup(m_RunPlaysGroupBoxLayout.createSequentialGroup()
                .addComponent(m_Run2PictureBox, javax.swing.GroupLayout.PREFERRED_SIZE, 74, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(m_Run2UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
            .addGroup(m_RunPlaysGroupBoxLayout.createSequentialGroup()
                .addComponent(m_Run3PictureBox, javax.swing.GroupLayout.PREFERRED_SIZE, 74, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(m_Run3UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
            .addGroup(m_RunPlaysGroupBoxLayout.createSequentialGroup()
                .addComponent(m_Run4PictureBox, javax.swing.GroupLayout.PREFERRED_SIZE, 74, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(m_Run4UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
        );

        m_PassPlaysGroupBox.setBorder(javax.swing.BorderFactory.createTitledBorder("Pass Plays"));

        m_Pass1PictureBox.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/plays/P1-0.png"))); // NOI18N

        m_Pass1UpDown.setModel(new javax.swing.SpinnerNumberModel(1, 1, 8, 1));

        m_Pass2UpDown.setModel(new javax.swing.SpinnerNumberModel(1, 1, 8, 1));

        m_Pass2PictureBox.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/plays/P1-0.png"))); // NOI18N

        m_Pass3UpDown.setModel(new javax.swing.SpinnerNumberModel(1, 1, 8, 1));

        m_Pass3PictureBox.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/plays/P1-0.png"))); // NOI18N

        m_Pass4UpDown.setModel(new javax.swing.SpinnerNumberModel(1, 1, 8, 1));

        m_Pass4PictureBox.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/plays/P1-0.png"))); // NOI18N

        javax.swing.GroupLayout m_PassPlaysGroupBoxLayout = new javax.swing.GroupLayout(m_PassPlaysGroupBox);
        m_PassPlaysGroupBox.setLayout(m_PassPlaysGroupBoxLayout);
        m_PassPlaysGroupBoxLayout.setHorizontalGroup(
            m_PassPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(m_PassPlaysGroupBoxLayout.createSequentialGroup()
                .addGroup(m_PassPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(m_Pass1PictureBox, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(m_Pass1UpDown))
                .addGap(31, 31, 31)
                .addGroup(m_PassPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(m_Pass2PictureBox, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(m_Pass2UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(31, 31, 31)
                .addGroup(m_PassPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(m_Pass3PictureBox, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(m_Pass3UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 30, Short.MAX_VALUE)
                .addGroup(m_PassPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(m_Pass4PictureBox, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(m_Pass4UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(14, 14, 14))
        );
        m_PassPlaysGroupBoxLayout.setVerticalGroup(
            m_PassPlaysGroupBoxLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(m_PassPlaysGroupBoxLayout.createSequentialGroup()
                .addComponent(m_Pass1PictureBox, javax.swing.GroupLayout.PREFERRED_SIZE, 74, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(m_Pass1UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
            .addGroup(m_PassPlaysGroupBoxLayout.createSequentialGroup()
                .addComponent(m_Pass2PictureBox, javax.swing.GroupLayout.PREFERRED_SIZE, 74, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(m_Pass2UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
            .addGroup(m_PassPlaysGroupBoxLayout.createSequentialGroup()
                .addComponent(m_Pass3PictureBox, javax.swing.GroupLayout.PREFERRED_SIZE, 74, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(m_Pass3UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
            .addGroup(m_PassPlaysGroupBoxLayout.createSequentialGroup()
                .addComponent(m_Pass4PictureBox, javax.swing.GroupLayout.PREFERRED_SIZE, 74, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(m_Pass4UpDown, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
        );

        m_SaveTeamButton.setText("Save Team Data");
        m_SaveTeamButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                m_SaveTeamButtonActionPerformed(evt);
            }
        });

        m_OKButton.setText("OK");
        m_OKButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                m_OKButtonActionPerformed(evt);
            }
        });

        m_CancelButton.setText("Cancel");
        m_CancelButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                m_CancelButtonActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                                .addComponent(m_TeamsComboBox, javax.swing.GroupLayout.PREFERRED_SIZE, 91, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addGap(24, 24, 24))
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(jLabel1)
                                .addGap(83, 83, 83)))
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(jLabel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addGap(23, 23, 23))
                            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                                .addComponent(m_SimOffenseUpDown, javax.swing.GroupLayout.PREFERRED_SIZE, 47, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addGap(19, 19, 19)))
                        .addGap(6, 6, 6)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(m_SimDefenseUpDown, javax.swing.GroupLayout.PREFERRED_SIZE, 47, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel4, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(m_OffensivePrefomComboBox, javax.swing.GroupLayout.PREFERRED_SIZE, 128, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addComponent(jLabel3, javax.swing.GroupLayout.PREFERRED_SIZE, 91, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addGap(6, 6, 6))
                            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(m_FormationComboBox, javax.swing.GroupLayout.PREFERRED_SIZE, 128, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addComponent(jLabel5, javax.swing.GroupLayout.PREFERRED_SIZE, 91, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addContainerGap())))
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                            .addComponent(m_RunPlaysGroupBox, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addGroup(javax.swing.GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                                .addGap(6, 6, 6)
                                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                                    .addComponent(m_PassPlaysGroupBox, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addGroup(layout.createSequentialGroup()
                                        .addComponent(m_SaveTeamButton, javax.swing.GroupLayout.PREFERRED_SIZE, 151, javax.swing.GroupLayout.PREFERRED_SIZE)
                                        .addGap(102, 102, 102)
                                        .addComponent(m_OKButton, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))))
                        .addGap(37, 37, 37)
                        .addComponent(m_CancelButton, javax.swing.GroupLayout.DEFAULT_SIZE, 89, Short.MAX_VALUE)
                        .addContainerGap())))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel4, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addGroup(layout.createSequentialGroup()
                                .addGap(14, 14, 14)
                                .addComponent(jLabel1)))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(m_TeamsComboBox, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(m_SimOffenseUpDown, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(m_SimDefenseUpDown, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(jLabel3)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(m_OffensivePrefomComboBox, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(jLabel5)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(m_FormationComboBox, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(m_RunPlaysGroupBox, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(m_PassPlaysGroupBox, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(m_SaveTeamButton)
                    .addComponent(m_OKButton)
                    .addComponent(m_CancelButton))
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void m_TeamsComboBoxActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_m_TeamsComboBoxActionPerformed
        if(m_InitDone)
        {
            ShowTeamValues();
        }
    }//GEN-LAST:event_m_TeamsComboBoxActionPerformed

    private void m_OffensivePrefomComboBoxActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_m_OffensivePrefomComboBoxActionPerformed
        UpdateData();
    }//GEN-LAST:event_m_OffensivePrefomComboBoxActionPerformed

    private void m_FormationComboBoxActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_m_FormationComboBoxActionPerformed
        UpdateData();
    }//GEN-LAST:event_m_FormationComboBoxActionPerformed

    private void m_SaveTeamButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_m_SaveTeamButtonActionPerformed
        UpdateData();
    }//GEN-LAST:event_m_SaveTeamButtonActionPerformed

    private void m_OKButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_m_OKButtonActionPerformed
        mCanceled = false; 
        setVisible(false);
    }//GEN-LAST:event_m_OKButtonActionPerformed

    private void m_CancelButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_m_CancelButtonActionPerformed
        mCanceled = true; 
        setVisible(false);
    }//GEN-LAST:event_m_CancelButtonActionPerformed

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
            java.util.logging.Logger.getLogger(ModifyTeamForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(ModifyTeamForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(ModifyTeamForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(ModifyTeamForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the dialog */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                ModifyTeamForm dialog = new ModifyTeamForm(new javax.swing.JFrame(), true);
                dialog.addWindowListener(new java.awt.event.WindowAdapter() {
                    @Override
                    public void windowClosing(java.awt.event.WindowEvent e) {
                        System.exit(0);
                    }
                });
                dialog.setVisible(true);
            }
        });
    }
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JButton m_CancelButton;
    private javax.swing.JComboBox m_FormationComboBox;
    private javax.swing.JButton m_OKButton;
    private javax.swing.JComboBox m_OffensivePrefomComboBox;
    private javax.swing.JLabel m_Pass1PictureBox;
    private javax.swing.JSpinner m_Pass1UpDown;
    private javax.swing.JLabel m_Pass2PictureBox;
    private javax.swing.JSpinner m_Pass2UpDown;
    private javax.swing.JLabel m_Pass3PictureBox;
    private javax.swing.JSpinner m_Pass3UpDown;
    private javax.swing.JLabel m_Pass4PictureBox;
    private javax.swing.JSpinner m_Pass4UpDown;
    private javax.swing.JPanel m_PassPlaysGroupBox;
    private javax.swing.JLabel m_Run1PictureBox;
    private javax.swing.JSpinner m_Run1UpDown;
    private javax.swing.JLabel m_Run2PictureBox;
    private javax.swing.JSpinner m_Run2UpDown;
    private javax.swing.JLabel m_Run3PictureBox;
    private javax.swing.JSpinner m_Run3UpDown;
    private javax.swing.JLabel m_Run4PictureBox;
    private javax.swing.JSpinner m_Run4UpDown;
    private javax.swing.JPanel m_RunPlaysGroupBox;
    private javax.swing.JButton m_SaveTeamButton;
    private javax.swing.JSpinner m_SimDefenseUpDown;
    private javax.swing.JSpinner m_SimOffenseUpDown;
    private javax.swing.JComboBox m_TeamsComboBox;
    // End of variables declaration//GEN-END:variables
}
