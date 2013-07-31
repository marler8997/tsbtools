/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package tsbtool_gui;

import java.awt.event.MouseAdapter;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import javax.swing.JCheckBox;
import javax.swing.JOptionPane;

/**
 *
 * @author Chris
 */
public class CXRomUniformUsageForm extends javax.swing.JDialog {

    /**
     * Creates new form CXRomUniformUsageForm
     */
    public CXRomUniformUsageForm(java.awt.Frame parent, boolean modal) {
        super(parent, modal);
        initComponents();
        checkBox29.setVisible(false);
        checkBox30.setVisible(false);

        mCheckBoxes[0] = checkBox1;
        mCheckBoxes[1] = checkBox2;
        mCheckBoxes[2] = checkBox3;
        mCheckBoxes[3] = checkBox4;
        mCheckBoxes[4] = checkBox5;
        mCheckBoxes[5] = checkBox6;
        mCheckBoxes[6] = checkBox7;
        mCheckBoxes[7] = checkBox8;
        mCheckBoxes[8] = checkBox9;
        mCheckBoxes[9] = checkBox10;
        mCheckBoxes[10] = checkBox11;
        mCheckBoxes[11] = checkBox12;
        mCheckBoxes[12] = checkBox13;
        mCheckBoxes[13] = checkBox14;
        mCheckBoxes[14] = checkBox15;
        mCheckBoxes[15] = checkBox16;
        mCheckBoxes[16] = checkBox17;
        mCheckBoxes[17] = checkBox18;
        mCheckBoxes[18] = checkBox19;
        mCheckBoxes[19] = checkBox20;
        mCheckBoxes[20] = checkBox21;
        mCheckBoxes[21] = checkBox22;
        mCheckBoxes[22] = checkBox23;
        mCheckBoxes[23] = checkBox24;
        mCheckBoxes[24] = checkBox25;
        mCheckBoxes[25] = checkBox26;
        mCheckBoxes[26] = checkBox27;
        mCheckBoxes[27] = checkBox28;
        mCheckBoxes[28] = checkBox29;
        mCheckBoxes[29] = checkBox30;
        mCheckBoxes[30] = checkBox31;
        mCheckBoxes[31] = checkBox32;
        
        MouseAdapter ma = new java.awt.event.MouseAdapter() {
            @Override
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                UpdateResult();
            }
        };
        for(int i = 0; i < mCheckBoxes.length; i++){
            mCheckBoxes[i].addMouseListener(ma);
        }
        
        jLabel1.setComponentPopupMenu(popupMenu);
        
        UpdateResult();
    }
    
    private JCheckBox[] mCheckBoxes = new JCheckBox[32];

    public String getStringValue() {
        return mResultTextBox.getText();
    }

    public void setStringValue(String value) {
        Matcher m = mStringValueRegex.matcher(value);
        if (m.find()) {
            String strVal = m.group(1);
            mResultTextBox.setText(strVal);
            long stuff = 0x00000000;
            boolean error = false;
            try {
                stuff = Long.parseLong(mResultTextBox.getText(), 16);
            } catch (Exception e) {
                error = true;
                JOptionPane.showMessageDialog(this,
                        String.format("Error setting up form, bad data '%s'", value),
                        "Error!", JOptionPane.ERROR_MESSAGE);
            }
            if (!error) {
                SetupCheckBoxes(stuff);
                if (!updating) {
                    UpdateResult();
                }
            }
        }
    }
    private Pattern mStringValueRegex = Pattern.compile("([0-9a-fA-F]{1,8})");

    private void SetAllBoxes(boolean val) {
        for (int i = 0; i < mCheckBoxes.length; i++) {
            if( mCheckBoxes[i].isVisible())
                mCheckBoxes[i].setSelected(val);
        }
    }

    private void SetupCheckBoxes(long data) {
        SetAllBoxes(false);

        if ((data & 0x80000000L) > 0) {
            mCheckBoxes[0].setSelected(true);
        }
        if ((data & 0x40000000L) > 0) {
            mCheckBoxes[1].setSelected(true);
        }
        if ((data & 0x20000000L) > 0) {
            mCheckBoxes[2].setSelected(true);
        }
        if ((data & 0x10000000L) > 0) {
            mCheckBoxes[3].setSelected(true);
        }
        if ((data & 0x08000000L) > 0) {
            mCheckBoxes[4].setSelected(true);
        }
        if ((data & 0x04000000L) > 0) {
            mCheckBoxes[5].setSelected(true);
        }
        if ((data & 0x02000000L) > 0) {
            mCheckBoxes[6].setSelected(true);
        }
        if ((data & 0x01000000L) > 0) {
            mCheckBoxes[7].setSelected(true);
        }
        if ((data & 0x00800000L) > 0) {
            mCheckBoxes[8].setSelected(true);
        }
        if ((data & 0x00400000L) > 0) {
            mCheckBoxes[9].setSelected(true);
        }
        if ((data & 0x00200000L) > 0) {
            mCheckBoxes[10].setSelected(true);
        }
        if ((data & 0x00100000L) > 0) {
            mCheckBoxes[11].setSelected(true);
        }
        if ((data & 0x00080000L) > 0) {
            mCheckBoxes[12].setSelected(true);
        }
        if ((data & 0x00040000L) > 0) {
            mCheckBoxes[13].setSelected(true);
        }
        if ((data & 0x00020000L) > 0) {
            mCheckBoxes[14].setSelected(true);
        }
        if ((data & 0x00010000L) > 0) {
            mCheckBoxes[15].setSelected(true);
        }
        if ((data & 0x00008000L) > 0) {
            mCheckBoxes[16].setSelected(true);
        }
        if ((data & 0x00004000L) > 0) {
            mCheckBoxes[17].setSelected(true);
        }
        if ((data & 0x00002000L) > 0) {
            mCheckBoxes[18].setSelected(true);
        }
        if ((data & 0x00001000L) > 0) {
            mCheckBoxes[19].setSelected(true);
        }
        if ((data & 0x00000800L) > 0) {
            mCheckBoxes[20].setSelected(true);
        }
        if ((data & 0x00000400L) > 0) {
            mCheckBoxes[21].setSelected(true);
        }
        if ((data & 0x00000200L) > 0) {
            mCheckBoxes[22].setSelected(true);
        }
        if ((data & 0x00000100L) > 0) {
            mCheckBoxes[23].setSelected(true);
        }
        if ((data & 0x00000080L) > 0) {
            mCheckBoxes[24].setSelected(true);
        }
        if ((data & 0x00000040L) > 0) {
            mCheckBoxes[25].setSelected(true);
        }
        if ((data & 0x00000020L) > 0) {
            mCheckBoxes[26].setSelected(true);
        }
        if ((data & 0x00000010L) > 0) {
            mCheckBoxes[27].setSelected(true);
        }

        if ((data & 0x00000008L) > 0) {
            mCheckBoxes[28].setSelected(true);
        }
        if ((data & 0x00000004L) > 0) {
            mCheckBoxes[29].setSelected(true);
        }
        if ((data & 0x00000002L) > 0) {
            mCheckBoxes[30].setSelected(true);
        }

        if ((data & 0x00000001L) > 0) {
            mCheckBoxes[31].setSelected(true);
        }
    }

    public long getValue() {
        long result = 0x00000000L;
        if (mCheckBoxes[0].isSelected()) {
            result += 0x80000000L;
        }
        if (mCheckBoxes[1].isSelected()) {
            result += 0x40000000L;
        }
        if (mCheckBoxes[2].isSelected()) {
            result += 0x20000000L;
        }
        if (mCheckBoxes[3].isSelected()) {
            result += 0x10000000L;
        }
        if (mCheckBoxes[4].isSelected()) {
            result += 0x08000000L;
        }
        if (mCheckBoxes[5].isSelected()) {
            result += 0x04000000L;
        }
        if (mCheckBoxes[6].isSelected()) {
            result += 0x02000000L;
        }
        if (mCheckBoxes[7].isSelected()) {
            result += 0x01000000L;
        }
        if (mCheckBoxes[8].isSelected()) {
            result += 0x00800000L;
        }
        if (mCheckBoxes[9].isSelected()) {
            result += 0x00400000L;
        }
        if (mCheckBoxes[10].isSelected()) {
            result += 0x00200000L;
        }
        if (mCheckBoxes[11].isSelected()) {
            result += 0x00100000L;
        }
        if (mCheckBoxes[12].isSelected()) {
            result += 0x00080000L;
        }
        if (mCheckBoxes[13].isSelected()) {
            result += 0x00040000L;
        }
        if (mCheckBoxes[14].isSelected()) {
            result += 0x00020000L;
        }
        if (mCheckBoxes[15].isSelected()) {
            result += 0x00010000L;
        }
        if (mCheckBoxes[16].isSelected()) {
            result += 0x00008000L;
        }
        if (mCheckBoxes[17].isSelected()) {
            result += 0x00004000L;
        }
        if (mCheckBoxes[18].isSelected()) {
            result += 0x00002000L;
        }
        if (mCheckBoxes[19].isSelected()) {
            result += 0x00001000L;
        }
        if (mCheckBoxes[20].isSelected()) {
            result += 0x00000800L;
        }
        if (mCheckBoxes[21].isSelected()) {
            result += 0x00000400L;
        }
        if (mCheckBoxes[22].isSelected()) {
            result += 0x00000200L;
        }
        if (mCheckBoxes[23].isSelected()) {
            result += 0x00000100L;
        }
        if (mCheckBoxes[24].isSelected()) {
            result += 0x00000080L;
        }
        if (mCheckBoxes[25].isSelected()) {
            result += 0x00000040L;
        }
        if (mCheckBoxes[26].isSelected()) {
            result += 0x00000020L;
        }
        if (mCheckBoxes[27].isSelected()) {
            result += 0x00000010L;
        }

        if (mCheckBoxes[28].isSelected()) {
            result += 0x00000008L;
        }
        if (mCheckBoxes[29].isSelected()) {
            result += 0x00000004L;
        }
        if (mCheckBoxes[30].isSelected()) {
            result += 0x00000002L;
        }
        if (mCheckBoxes[31].isSelected()) {
            result += 0x00000001L;
        }

        return result;
    }

    private void UpdateResult() {
        if (!updating) {
            long tVal = getValue();
            String sVal = String.format("%08X", tVal);
            mResultTextBox.setText(sVal);
        }
    }
    private boolean updating = false;
    private boolean mCanceled = false;

    /**
     *
     * @return true if the user canceled the dialog, false if confirmed.
     */
    public boolean getCanceled() {
        return mCanceled;
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        popupMenu = new javax.swing.JPopupMenu();
        checkAllMenuItem = new javax.swing.JMenuItem();
        uncheckAllMenuItem = new javax.swing.JMenuItem();
        checkBox1 = new javax.swing.JCheckBox();
        checkBox2 = new javax.swing.JCheckBox();
        checkBox3 = new javax.swing.JCheckBox();
        checkBox4 = new javax.swing.JCheckBox();
        checkBox5 = new javax.swing.JCheckBox();
        checkBox6 = new javax.swing.JCheckBox();
        checkBox7 = new javax.swing.JCheckBox();
        checkBox8 = new javax.swing.JCheckBox();
        checkBox9 = new javax.swing.JCheckBox();
        checkBox10 = new javax.swing.JCheckBox();
        checkBox11 = new javax.swing.JCheckBox();
        checkBox12 = new javax.swing.JCheckBox();
        checkBox13 = new javax.swing.JCheckBox();
        checkBox14 = new javax.swing.JCheckBox();
        checkBox15 = new javax.swing.JCheckBox();
        checkBox16 = new javax.swing.JCheckBox();
        checkBox17 = new javax.swing.JCheckBox();
        checkBox18 = new javax.swing.JCheckBox();
        checkBox19 = new javax.swing.JCheckBox();
        checkBox20 = new javax.swing.JCheckBox();
        checkBox21 = new javax.swing.JCheckBox();
        checkBox22 = new javax.swing.JCheckBox();
        checkBox23 = new javax.swing.JCheckBox();
        checkBox24 = new javax.swing.JCheckBox();
        checkBox25 = new javax.swing.JCheckBox();
        checkBox26 = new javax.swing.JCheckBox();
        checkBox27 = new javax.swing.JCheckBox();
        checkBox28 = new javax.swing.JCheckBox();
        checkBox29 = new javax.swing.JCheckBox();
        checkBox30 = new javax.swing.JCheckBox();
        checkBox31 = new javax.swing.JCheckBox();
        checkBox32 = new javax.swing.JCheckBox();
        jLabel1 = new javax.swing.JLabel();
        mResultTextBox = new javax.swing.JTextField();
        mOKButton = new javax.swing.JButton();
        mCancelButton = new javax.swing.JButton();

        checkAllMenuItem.setText("Check All");
        checkAllMenuItem.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                checkAllMenuItemActionPerformed(evt);
            }
        });
        popupMenu.add(checkAllMenuItem);

        uncheckAllMenuItem.setText("Uncheck All");
        uncheckAllMenuItem.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                uncheckAllMenuItemActionPerformed(evt);
            }
        });
        popupMenu.add(uncheckAllMenuItem);

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setMaximumSize(new java.awt.Dimension(540, 453));
        setMinimumSize(new java.awt.Dimension(540, 453));
        setPreferredSize(new java.awt.Dimension(540, 453));
        getContentPane().setLayout(null);

        checkBox1.setBackground(new java.awt.Color(0, 113, 239));
        checkBox1.setOpaque(true);
        getContentPane().add(checkBox1);
        checkBox1.setBounds(0, 20, 20, 18);

        checkBox2.setBackground(new java.awt.Color(0, 113, 239));
        checkBox2.setOpaque(true);
        getContentPane().add(checkBox2);
        checkBox2.setBounds(0, 50, 20, 18);

        checkBox3.setBackground(new java.awt.Color(0, 113, 239));
        checkBox3.setOpaque(true);
        getContentPane().add(checkBox3);
        checkBox3.setBounds(0, 80, 20, 18);

        checkBox4.setBackground(new java.awt.Color(0, 113, 239));
        checkBox4.setOpaque(true);
        getContentPane().add(checkBox4);
        checkBox4.setBounds(0, 110, 20, 18);

        checkBox5.setBackground(new java.awt.Color(0, 113, 239));
        checkBox5.setOpaque(true);
        getContentPane().add(checkBox5);
        checkBox5.setBounds(120, 20, 20, 18);

        checkBox6.setBackground(new java.awt.Color(0, 113, 239));
        checkBox6.setOpaque(true);
        getContentPane().add(checkBox6);
        checkBox6.setBounds(120, 50, 20, 18);

        checkBox7.setBackground(new java.awt.Color(0, 113, 239));
        checkBox7.setOpaque(true);
        getContentPane().add(checkBox7);
        checkBox7.setBounds(120, 90, 20, 18);

        checkBox8.setBackground(new java.awt.Color(0, 113, 239));
        checkBox8.setOpaque(true);
        getContentPane().add(checkBox8);
        checkBox8.setBounds(120, 120, 20, 18);

        checkBox9.setBackground(new java.awt.Color(0, 113, 239));
        checkBox9.setOpaque(true);
        getContentPane().add(checkBox9);
        checkBox9.setBounds(250, 20, 20, 18);

        checkBox10.setBackground(new java.awt.Color(0, 113, 239));
        checkBox10.setOpaque(true);
        getContentPane().add(checkBox10);
        checkBox10.setBounds(250, 60, 20, 18);

        checkBox11.setBackground(new java.awt.Color(0, 113, 239));
        checkBox11.setOpaque(true);
        getContentPane().add(checkBox11);
        checkBox11.setBounds(250, 90, 20, 18);

        checkBox12.setBackground(new java.awt.Color(0, 113, 239));
        checkBox12.setOpaque(true);
        getContentPane().add(checkBox12);
        checkBox12.setBounds(250, 120, 20, 18);

        checkBox13.setBackground(new java.awt.Color(0, 113, 239));
        checkBox13.setOpaque(true);
        getContentPane().add(checkBox13);
        checkBox13.setBounds(380, 20, 20, 18);

        checkBox14.setBackground(new java.awt.Color(0, 113, 239));
        checkBox14.setOpaque(true);
        getContentPane().add(checkBox14);
        checkBox14.setBounds(380, 60, 20, 18);

        checkBox15.setBackground(new java.awt.Color(0, 113, 239));
        checkBox15.setOpaque(true);
        getContentPane().add(checkBox15);
        checkBox15.setBounds(380, 90, 20, 18);

        checkBox16.setBackground(new java.awt.Color(0, 113, 239));
        checkBox16.setOpaque(true);
        getContentPane().add(checkBox16);
        checkBox16.setBounds(380, 120, 20, 18);

        checkBox17.setBackground(new java.awt.Color(0, 113, 239));
        checkBox17.setOpaque(true);
        getContentPane().add(checkBox17);
        checkBox17.setBounds(0, 180, 20, 18);

        checkBox18.setBackground(new java.awt.Color(0, 113, 239));
        checkBox18.setOpaque(true);
        getContentPane().add(checkBox18);
        checkBox18.setBounds(0, 210, 20, 18);

        checkBox19.setBackground(new java.awt.Color(0, 113, 239));
        checkBox19.setOpaque(true);
        getContentPane().add(checkBox19);
        checkBox19.setBounds(0, 240, 20, 18);

        checkBox20.setBackground(new java.awt.Color(0, 113, 239));
        checkBox20.setOpaque(true);
        getContentPane().add(checkBox20);
        checkBox20.setBounds(0, 280, 20, 18);

        checkBox21.setBackground(new java.awt.Color(0, 113, 239));
        checkBox21.setOpaque(true);
        getContentPane().add(checkBox21);
        checkBox21.setBounds(120, 180, 20, 20);

        checkBox22.setBackground(new java.awt.Color(0, 113, 239));
        checkBox22.setOpaque(true);
        getContentPane().add(checkBox22);
        checkBox22.setBounds(120, 210, 20, 18);

        checkBox23.setBackground(new java.awt.Color(0, 113, 239));
        checkBox23.setOpaque(true);
        getContentPane().add(checkBox23);
        checkBox23.setBounds(120, 240, 20, 18);

        checkBox24.setBackground(new java.awt.Color(0, 113, 239));
        checkBox24.setOpaque(true);
        getContentPane().add(checkBox24);
        checkBox24.setBounds(120, 280, 20, 18);

        checkBox25.setBackground(new java.awt.Color(0, 113, 239));
        checkBox25.setOpaque(true);
        getContentPane().add(checkBox25);
        checkBox25.setBounds(250, 180, 20, 18);

        checkBox26.setBackground(new java.awt.Color(0, 113, 239));
        checkBox26.setOpaque(true);
        getContentPane().add(checkBox26);
        checkBox26.setBounds(250, 210, 20, 18);

        checkBox27.setBackground(new java.awt.Color(0, 113, 239));
        checkBox27.setOpaque(true);
        getContentPane().add(checkBox27);
        checkBox27.setBounds(250, 250, 20, 18);

        checkBox28.setBackground(new java.awt.Color(0, 113, 239));
        checkBox28.setOpaque(true);
        getContentPane().add(checkBox28);
        checkBox28.setBounds(250, 280, 20, 18);

        checkBox29.setBackground(new java.awt.Color(0, 113, 239));
        checkBox29.setOpaque(true);
        getContentPane().add(checkBox29);
        checkBox29.setBounds(300, 150, 20, 18);

        checkBox30.setBackground(new java.awt.Color(0, 113, 239));
        checkBox30.setOpaque(true);
        getContentPane().add(checkBox30);
        checkBox30.setBounds(330, 150, 20, 18);

        checkBox31.setBackground(new java.awt.Color(0, 113, 239));
        checkBox31.setOpaque(true);
        getContentPane().add(checkBox31);
        checkBox31.setBounds(380, 180, 20, 18);

        checkBox32.setBackground(new java.awt.Color(0, 113, 239));
        checkBox32.setOpaque(true);
        getContentPane().add(checkBox32);
        checkBox32.setBounds(380, 210, 20, 18);

        jLabel1.setBackground(new java.awt.Color(0, 113, 239));
        jLabel1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/icons/TeamSelect32.PNG"))); // NOI18N
        jLabel1.setOpaque(true);
        getContentPane().add(jLabel1);
        jLabel1.setBounds(0, 0, 530, 350);
        getContentPane().add(mResultTextBox);
        mResultTextBox.setBounds(10, 360, 190, 28);

        mOKButton.setText("OK");
        mOKButton.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                mOKButtonMouseClicked(evt);
            }
        });
        getContentPane().add(mOKButton);
        mOKButton.setBounds(360, 370, 80, 28);

        mCancelButton.setText("Cancel");
        mCancelButton.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                mCancelButtonMouseClicked(evt);
            }
        });
        getContentPane().add(mCancelButton);
        mCancelButton.setBounds(450, 370, 70, 28);

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void mCancelButtonMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_mCancelButtonMouseClicked
        mCanceled = true;
        setVisible(false);
    }//GEN-LAST:event_mCancelButtonMouseClicked

    private void mOKButtonMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_mOKButtonMouseClicked
        mCanceled = false;
        setVisible(false);
    }//GEN-LAST:event_mOKButtonMouseClicked

    private void checkAllMenuItemActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_checkAllMenuItemActionPerformed
        SetAllBoxes(true);
        UpdateResult();
    }//GEN-LAST:event_checkAllMenuItemActionPerformed

    private void uncheckAllMenuItemActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_uncheckAllMenuItemActionPerformed
        SetAllBoxes(false);
        UpdateResult();
    }//GEN-LAST:event_uncheckAllMenuItemActionPerformed

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
            java.util.logging.Logger.getLogger(CXRomUniformUsageForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(CXRomUniformUsageForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(CXRomUniformUsageForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(CXRomUniformUsageForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the dialog */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                CXRomUniformUsageForm dialog = new CXRomUniformUsageForm(new javax.swing.JFrame(), true);
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
    private javax.swing.JMenuItem checkAllMenuItem;
    private javax.swing.JCheckBox checkBox1;
    private javax.swing.JCheckBox checkBox10;
    private javax.swing.JCheckBox checkBox11;
    private javax.swing.JCheckBox checkBox12;
    private javax.swing.JCheckBox checkBox13;
    private javax.swing.JCheckBox checkBox14;
    private javax.swing.JCheckBox checkBox15;
    private javax.swing.JCheckBox checkBox16;
    private javax.swing.JCheckBox checkBox17;
    private javax.swing.JCheckBox checkBox18;
    private javax.swing.JCheckBox checkBox19;
    private javax.swing.JCheckBox checkBox2;
    private javax.swing.JCheckBox checkBox20;
    private javax.swing.JCheckBox checkBox21;
    private javax.swing.JCheckBox checkBox22;
    private javax.swing.JCheckBox checkBox23;
    private javax.swing.JCheckBox checkBox24;
    private javax.swing.JCheckBox checkBox25;
    private javax.swing.JCheckBox checkBox26;
    private javax.swing.JCheckBox checkBox27;
    private javax.swing.JCheckBox checkBox28;
    private javax.swing.JCheckBox checkBox29;
    private javax.swing.JCheckBox checkBox3;
    private javax.swing.JCheckBox checkBox30;
    private javax.swing.JCheckBox checkBox31;
    private javax.swing.JCheckBox checkBox32;
    private javax.swing.JCheckBox checkBox4;
    private javax.swing.JCheckBox checkBox5;
    private javax.swing.JCheckBox checkBox6;
    private javax.swing.JCheckBox checkBox7;
    private javax.swing.JCheckBox checkBox8;
    private javax.swing.JCheckBox checkBox9;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JButton mCancelButton;
    private javax.swing.JButton mOKButton;
    private javax.swing.JTextField mResultTextBox;
    private javax.swing.JPopupMenu popupMenu;
    private javax.swing.JMenuItem uncheckAllMenuItem;
    // End of variables declaration//GEN-END:variables
}
